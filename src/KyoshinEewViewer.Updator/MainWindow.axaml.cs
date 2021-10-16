using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using KyoshinEewViewer.Core.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Threading.Tasks;

namespace KyoshinEewViewer.Updator
{
	public class MainWindow : Window
	{
		private HttpClient Client { get; } = new();
		private const string UpdateCheckUrl = "https://svs.ingen084.net/kyoshineewviewer/updates.json";
		private const string UpdateDirectory = "../";
		private const string SettingsFileName = "config.json";

		public MainWindow()
		{
			InitializeComponent();
#if DEBUG
			this.AttachDevTools();
#endif
			Client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "KEViUpdator;" + Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "Unknown");
			this.FindControl<Button>("closeButton").Tapped += (s, e) => Close();
			DoUpdate();
		}

		private async void DoUpdate()
		{
			var progress = this.FindControl<ProgressBar>("progress");
			progress.Value = 0;
			progress.IsIndeterminate = true;
			var progressText = this.FindControl<TextBlock>("progressText");
			progressText.Text = "";
			var infoText = this.FindControl<TextBlock>("infoText");
			var closeButton = this.FindControl<Button>("closeButton");

			try
			{
				while (Process.GetProcessesByName("KyoshinEewViewer").Any())
				{
					closeButton.IsEnabled = true;
					infoText.Text = "KyoshinEewViewer �̃v���Z�X���I������̂�҂��Ă��܂�";
					await Task.Delay(1000);
				}
				closeButton.IsEnabled = false;
				infoText.Text = "�K�p�\�ȍX�V���擾���Ă��܂�";

				// �A�v���ɂ��ۑ���҂��Ă���
				await Task.Delay(1000);
				if (!File.Exists(Path.Combine(UpdateDirectory, SettingsFileName)))
					throw new Exception("�ݒ�t�@�C����������܂���");
				if (JsonSerializer.Deserialize<KyoshinEewViewerConfiguration>(File.ReadAllText(Path.Combine(UpdateDirectory, SettingsFileName))) is not KyoshinEewViewerConfiguration config)
					throw new Exception("�ݒ�t�@�C����ǂݍ��ނ��Ƃ��ł��܂���");

				var version = JsonSerializer.Deserialize<VersionInfo[]>(await Client.GetStringAsync(UpdateCheckUrl))
					?.OrderByDescending(v => v.Version).Where(v => v.Version > config.SavedVersion).FirstOrDefault();

				if (string.IsNullOrWhiteSpace(version?.Url))
				{
					infoText.Text = "�K�p�\�ȍX�V�͂���܂���";
					progress.IsIndeterminate = false;
					closeButton.IsEnabled = true;
					return;
				}

				infoText.Text = $"�o�[�W���� {version.Version} �ɍX�V���s���܂�";

				var catalog = JsonSerializer.Deserialize<Dictionary<string, string>>(await Client.GetStringAsync(version.Url));
				if (catalog == null)
					throw new Exception("�A�b�v�f�[�g�J�^���O���擾�ł��܂���");

				if (!catalog.ContainsKey(RuntimeInformation.RuntimeIdentifier))
				{
					infoText.Text = "���݂̃v���b�g�t�H�[���Ŏ����X�V�͗��p�ł��܂���";
					progress.IsIndeterminate = false;
					closeButton.IsEnabled = true;
					return;
				}

				infoText.Text = $"�o�[�W���� {version.Version} ���_�E�����[�h���Ă��܂�";
				progress.IsIndeterminate = false;

				var tmpFileName = Path.GetTempFileName();
				// �_�E�����[�h�J�n
				using (var fileStream = File.OpenWrite(tmpFileName))
				{
					using var response = await Client.GetAsync(catalog[RuntimeInformation.RuntimeIdentifier], HttpCompletionOption.ResponseHeadersRead);
					progress.Maximum = response.Content.Headers.ContentLength ?? throw new Exception("DL�T�C�Y���擾�ł��܂���");

					using var inputStream = await response.Content.ReadAsStreamAsync();

					var total = 0;
					var buffer = new byte[1024];
					while (true)
					{
						var readed = await inputStream.ReadAsync(buffer);
						if (readed == 0)
							break;

						progress.Value = total += readed;
						progressText.Text = $"�_�E�����[�h��: {progress.Value / progress.Maximum * 100:0.00}%";

						await fileStream.WriteAsync(buffer, 0, readed);
					}
				}

				infoText.Text = "�t�@�C����W�J���Ă��܂�";
				progress.IsIndeterminate = true;
				progressText.Text = "";

				await Task.Run(() => ZipFile.ExtractToDirectory(tmpFileName, UpdateDirectory, true));
				File.Delete(tmpFileName);

				infoText.Text = "�X�V���������܂���";
				progress.IsIndeterminate = false;

				await Task.Delay(1000);

				infoText.Text = "�A�v���P�[�V�������N�����Ă��܂�";

				Process.Start(new ProcessStartInfo(Path.Combine(UpdateDirectory, "KyoshinEewViewer")) { WorkingDirectory = UpdateDirectory });

				await Task.Delay(2000);

				Close();
			}
			catch (Exception ex)
			{
				infoText.Text = "�X�V���ɖ�肪�������܂���";
				progressText.Text = ex.Message;
				progress.IsIndeterminate = false;
				closeButton.IsEnabled = true;
			}
		}

		private void InitializeComponent() => AvaloniaXamlLoader.Load(this);
	}
}
