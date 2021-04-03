﻿using KyoshinMonitorLib;
using Prism.Mvvm;
using System.Text.Json.Serialization;

namespace KyoshinEewViewer.Models
{
	public class KyoshinEewViewerConfiguration : BindableBase
	{
		private double windowScale = 1;
		public double WindowScale
		{
			get => windowScale;
			set => SetProperty(ref windowScale, value);
		}

		private TimerConfig timer = new TimerConfig();
		public TimerConfig Timer
		{
			get => timer;
			set => SetProperty(ref timer, value);
		}
		public class TimerConfig : BindableBase
		{
			private int offset = 1100;
			public int Offset
			{
				get => offset;
				set => SetProperty(ref offset, value);
			}
			private bool autoOffsetIncrement = true;
			public bool AutoOffsetIncrement
			{
				get => autoOffsetIncrement;
				set => SetProperty(ref autoOffsetIncrement, value);
			}

			private int timeshiftSeconds;
			[JsonIgnore]
			public int TimeshiftSeconds
			{
				get => timeshiftSeconds;
				set => SetProperty(ref timeshiftSeconds, value);
			}
		}

		private KyoshinMonitorConfig kyoshinMonitor = new KyoshinMonitorConfig();
		public KyoshinMonitorConfig KyoshinMonitor
		{
			get => kyoshinMonitor;
			set => SetProperty(ref kyoshinMonitor, value);
		}
		public class KyoshinMonitorConfig : BindableBase
		{
			private bool hideShindoIcon;
			public bool HideShindoIcon
			{
				get => hideShindoIcon;
				set => SetProperty(ref hideShindoIcon, value);
			}

			private int fetchFrequency = 1;
			public int FetchFrequency
			{
				get => fetchFrequency;
				set => SetProperty(ref fetchFrequency, value);
			}
			private bool forcefetchOnEew = false;
			public bool ForcefetchOnEew
			{
				get => forcefetchOnEew;
				set => SetProperty(ref forcefetchOnEew, value);
			}
		}

		private EewConfig eew = new EewConfig();
		public EewConfig Eew
		{
			get => eew;
			set => SetProperty(ref eew, value);
		}
		public class EewConfig : BindableBase
		{
			private bool enableLast10Second = false;
			public bool EnableLast10Second
			{
				get => enableLast10Second;
				set => SetProperty(ref enableLast10Second, value);
			}

			private bool enableSignalNowProfessional = false;
			public bool EnableSignalNowProfessional
			{
				get => enableSignalNowProfessional;
				set => SetProperty(ref enableSignalNowProfessional, value);
			}
		}

		private ThemeConfig theme = new ThemeConfig();
		public ThemeConfig Theme
		{
			get => theme;
			set => SetProperty(ref theme, value);
		}
		public class ThemeConfig : BindableBase
		{
			private string windowThemeName = "Light";
			public string WindowThemeName
			{
				get => windowThemeName;
				set => SetProperty(ref windowThemeName, value);
			}
			private string intensityThemeName = "Standard";
			public string IntensityThemeName
			{
				get => intensityThemeName;
				set => SetProperty(ref intensityThemeName, value);
			}
		}

		private NetworkTimeConfig networkTime = new NetworkTimeConfig();
		public NetworkTimeConfig NetworkTime
		{
			get => networkTime;
			set => SetProperty(ref networkTime, value);
		}
		public class NetworkTimeConfig : BindableBase
		{
			private bool enable = true;
			public bool Enable
			{
				get => enable;
				set => SetProperty(ref enable, value);
			}

			private string address = "ntp.nict.jp";
			public string Address
			{
				get => address;
				set => SetProperty(ref address, value);
			}
		}

		private LoggingConfig logging = new LoggingConfig();
		public LoggingConfig Logging
		{
			get => logging;
			set => SetProperty(ref logging, value);
		}
		public class LoggingConfig : BindableBase
		{
			private bool enable = false;
			public bool Enable
			{
				get => enable;
				set => SetProperty(ref enable, value);
			}
			private string directory = "Logs";
			public string Directory
			{
				get => directory;
				set => SetProperty(ref directory, value);
			}
		}

		private UpdateConfig update = new UpdateConfig();
		public UpdateConfig Update
		{
			get => update;
			set => SetProperty(ref update, value);
		}
		public class UpdateConfig : BindableBase
		{
			private bool enable = true;
			public bool Enable
			{
				get => enable;
				set => SetProperty(ref enable, value);
			}

			private bool useUnstableBuild;
			public bool UseUnstableBuild
			{
				get => useUnstableBuild;
				set => SetProperty(ref useUnstableBuild, value);
			}
		}

		private NotificationConfig notification = new NotificationConfig();
		public NotificationConfig Notification
		{
			get => notification;
			set => SetProperty(ref notification, value);
		}
		public class NotificationConfig : BindableBase
		{
			private bool enable = true;
			public bool Enable
			{
				get => enable;
				set => SetProperty(ref enable, value);
			}

			private bool hideWhenMinimizeWindow = true;
			public bool HideWhenMinimizeWindow
			{
				get => hideWhenMinimizeWindow;
				set => SetProperty(ref hideWhenMinimizeWindow, value);
			}

			private bool hideWhenClosingWindow;
			public bool HideWhenClosingWindow
			{
				get => hideWhenClosingWindow;
				set => SetProperty(ref hideWhenClosingWindow, value);
			}
		}

		private MapConfig map = new MapConfig();
		public MapConfig Map
		{
			get => map;
			set
			{
				if (value == null) //memo null対策
					return;
				SetProperty(ref map, value);
			}
		}
		public class MapConfig : BindableBase
		{
			private bool disableManualMapControl;
			public bool DisableManualMapControl
			{
				get => disableManualMapControl;
				set => SetProperty(ref disableManualMapControl, value);
			}

			private bool keepRegion;
			public bool KeepRegion
			{
				get => keepRegion;
				set => SetProperty(ref keepRegion, value);
			}

			private bool autoFocus;
			public bool AutoFocus
			{
				get => autoFocus;
				set => SetProperty(ref autoFocus, value);
			}

			private Location location1 = new Location(24.058240f, 123.046875f);
			public Location Location1
			{
				get => location1;
				set => SetProperty(ref location1, value);
			}
			private Location location2 = new Location(45.706479f, 146.293945f);
			public Location Location2
			{
				get => location2;
				set => SetProperty(ref location2, value);
			}

			private bool autoFocusAnimation = true;
			public bool AutoFocusAnimation
			{
				get => autoFocusAnimation;
				set => SetProperty(ref autoFocusAnimation, value);
			}
		}

		private DmdataConfig dmdata = new DmdataConfig();
		public DmdataConfig Dmdata
		{
			get => dmdata;
			set => SetProperty(ref dmdata, value);
		}
		public class DmdataConfig : BindableBase
		{
			private string apiKey = "";
			public string ApiKey
			{
				get => apiKey;
				set => SetProperty(ref apiKey, value);
			}

			private bool useWebSocket = true;
			public bool UseWebSocket
			{
				get => useWebSocket;
				set => SetProperty(ref useWebSocket, value);
			}

			private double pullMultiply = 1;
			public double PullMultiply
			{
				get => pullMultiply;
				set => SetProperty(ref pullMultiply, value);
			}
		}

		private RawIntensityObjectConfig rawIntensityObject = new RawIntensityObjectConfig();
		public RawIntensityObjectConfig RawIntensityObject
		{
			get => rawIntensityObject;
			set => SetProperty(ref rawIntensityObject, value);
		}
		public class RawIntensityObjectConfig : BindableBase
		{
			private double showNameZoomLevel = 9;
			public double ShowNameZoomLevel
			{
				get => showNameZoomLevel;
				set => SetProperty(ref showNameZoomLevel, value);
			}
			private double showValueZoomLevel = 9.5;
			public double ShowValueZoomLevel
			{
				get => showValueZoomLevel;
				set => SetProperty(ref showValueZoomLevel, value);
			}

			private double minShownShindo = -3;
			public double MinShownIntensity
			{
				get => minShownShindo;
				set => SetProperty(ref minShownShindo, value);
			}

			private bool showIntensityIcon = false;
			public bool ShowIntensityIcon
			{
				get => showIntensityIcon;
				set => SetProperty(ref showIntensityIcon, value);
			}

			private bool showInvalidateIcon = true;
			public bool ShowInvalidateIcon
			{
				get => showInvalidateIcon;
				set => SetProperty(ref showInvalidateIcon, value);
			}
		}
	}
}
