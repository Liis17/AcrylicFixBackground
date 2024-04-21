# AcrylicFixBackground
Шаблон проекта основаном на **[wpfui](https://github.com/lepoco/wpfui)** в котором реализована смена цвета фона окна в зависимости от активности окна, для активного и неактивного состояния отдельно могут задаваться цвет, прозрачность и время переключения

---
### Стандартное состояние
![](https://github.com/Liis17/AcrylicFixBackground/blob/master/Files/Gif/NVIDIA_Overlay_8MuGeLrKp7.gif)
### Моё наговнокоженное решение )
![](https://github.com/Liis17/AcrylicFixBackground/blob/master/Files/Gif/NVIDIA_Overlay_AI9K89bAbp.gif)

---

## Использовние
Взять этот проект как основу своего или перенести это в свой проект 
### Установить сам **[wpfui](https://github.com/lepoco/wpfui)**

В **App.xaml** вставть:
```xml
<Application
  ...
  xmlns:ui="http://schemas.lepo.co/wpfui/2022/xaml">
  <Application.Resources>
    <ResourceDictionary>
      <ResourceDictionary.MergedDictionaries>
        <ui:ThemesDictionary Theme="Dark" />
        <ui:ControlsDictionary />
      </ResourceDictionary.MergedDictionaries>
    </ResourceDictionary>
  </Application.Resources>
</Application>
```

В **MainWindow.xaml** вставить:
   ```xml
<ui:FluentWindow
  ...
  xmlns:ui="http://schemas.lepo.co/wpfui/2022/xaml">
  ...
</ui:FluentWindow>
```

Так же в **MainWindow.xaml.cs** заменить:
```C#
public partial class MainWindow : Window
```
на
```C#
public partial class MainWindow : FluentWindow
```

---

### Теперь перенос того что я наговнокодил для смены цвета
В **MainWindow.xaml.cs** вставить:
```C#
public Color MWColorBgroundActive = Color.FromRgb(0, 0, 0); 
public Color MWColorBgroundInactive = Color.FromRgb(255, 192, 203); 
public double TransparencyActive = 0.0; 
public double TransparencyInactive = 1; 
public TimeSpan ColorSwitchTimeActive = TimeSpan.FromMilliseconds(150); 
public TimeSpan ColorSwitchTimeInactive = TimeSpan.FromMilliseconds(270); 
```
```
MWColorBgroundActive - цвет фона при активном состоянии окна
MWColorBgroundInactive - цвет фона при неактивном состоянии окна
TransparencyActive - прозрачность фона при активном состоянии окна
TransparencyInactive - прозрачность фона при неактивном состоянии окна
ColorSwitchTimeActive - время смены цвета на активное состояния окна
ColorSwitchTimeInactive - время смены цвета на неактивное состояние окна
```
А так же весь остальной код из **MainWindow.xaml.cs**:
```C#
private void ChangeBackgroundColor(Brush targetBrush, TimeSpan duration) { ... }
private Brush ChangeOpacity(Color color, double opacity) { ... }
private void FluentWindow_Activated(object sender, EventArgs e) { ... }
private void FluentWindow_Deactivated(object sender, EventArgs e) { ... }
```
Для возможности перетаскивать окно за любое место окна добавить:
```C#
private void WindowMouseDown(object sender, MouseButtonEventArgs e){ ... }
```
В **MainWindow.xaml** вставить:
```xml
Activated="FluentWindow_Activated"
Deactivated="FluentWindow_Deactivated"
ExtendsContentIntoTitleBar="True"
WindowBackdropType="Acrylic"
```
Так же для перетаскивания окно за любое место окна добавить:
```xml
MouseDown="WindowMouseDown"
```
Еще по желанию:
```xml
WindowStartupLocation="CenterScreen"
WindowStyle="None"
```
А так же при необходимости:
```xml
...
<Grid>
<ui:TitleBar HorizontalAlignment="Right" VerticalAlignment="Top" />
...
</Grid>
```
