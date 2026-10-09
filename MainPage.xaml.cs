namespace GamerSurvey;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    /// <summary>
    /// Кнопка «Показать результат»: проверяет обязательные поля и выводит сводку.
    /// </summary>
    private async void OnShowResultClicked(object sender, EventArgs e)
    {
        // --- Валидация минимум двух обязательных полей ---
        string nickname = entryNickname.Text?.Trim() ?? "";
        string email = entryEmail.Text?.Trim() ?? "";

        if (string.IsNullOrEmpty(nickname))
        {
            await DisplayAlertAsync("Ошибка", "Введите никнейм!", "OK");
            return;
        }

        if (string.IsNullOrEmpty(email))
        {
            await DisplayAlertAsync("Ошибка", "Введите email!", "OK");
            return;
        }

        // --- Сбор данных ---

        // Дата рождения
        string birthDate = dpBirthDate.Date?.ToString("dd.MM.yyyy") ?? "не указана";

        // Платформа (RadioButton)
        string platform = "не выбрана";
        if (rbPC.IsChecked) platform = "PC";
        else if (rbConsole.IsChecked) platform = "Консоль";
        else if (rbMobile.IsChecked) platform = "Мобильное устройство";

        // Жанр (Picker)
        string genre = pickerGenre.SelectedIndex >= 0
            ? pickerGenre.SelectedItem?.ToString() ?? "не выбран"
            : "не выбран";

        // Часы игры (Slider)
        double hours = sliderHours.Value;

        // Чекбоксы
        string multiplayer = cbMultiplayer.IsChecked ? "Да" : "Нет";
        string streaming = cbStreaming.IsChecked ? "Да" : "Нет";

        // Switch
        string notifications = switchNotifications.IsToggled ? "Да" : "Нет";

        // Комментарий (Editor)
        string comment = editorComment.Text?.Trim() ?? "—";
        if (string.IsNullOrEmpty(comment)) comment = "—";

        // --- Формирование сводки ---
        string summary =
            $"👤 Никнейм: {nickname}\n" +
            $"📧 Email: {email}\n" +
            $"🎂 Дата рождения: {birthDate}\n" +
            $"🖥 Платформа: {platform}\n" +
            $"🎯 Любимый жанр: {genre}\n" +
            $"⏰ Часов в день: {hours:F1}\n" +
            $"🌐 Мультиплеер: {multiplayer}\n" +
            $"📹 Стриминг: {streaming}\n" +
            $"🔔 Уведомления: {notifications}\n" +
            $"💬 Комментарий: {comment}";

        lblResult.Text = summary;

        // --- Изменение IsVisible в зависимости от заполнения ---
        frameResult.IsVisible = true;

        // --- Изменение IsEnabled: блокируем кнопку после отправки ---
        btnResult.IsEnabled = false;
        btnResult.Text = "✅ Отправлено";
    }

    /// <summary>
    /// Кнопка «Очистить»: возвращает все поля и результат в исходное состояние.
    /// </summary>
    private void OnClearClicked(object sender, EventArgs e)
    {
        // Entry
        entryNickname.Text = "";
        entryEmail.Text = "";

        // DatePicker — начальное значение внутри диапазона
        dpBirthDate.Date = new DateTime(2000, 1, 15);

        // RadioButton — сброс
        rbPC.IsChecked = false;
        rbConsole.IsChecked = false;
        rbMobile.IsChecked = false;

        // Picker
        pickerGenre.SelectedIndex = -1;

        // Slider — начальное значение внутри диапазона
        sliderHours.Value = 3;

        // CheckBox
        cbMultiplayer.IsChecked = false;
        cbStreaming.IsChecked = false;

        // Switch
        switchNotifications.IsToggled = true;

        // Editor
        editorComment.Text = "";

        // Результат
        lblResult.Text = "";
        frameResult.IsVisible = false;

        // Кнопка — восстанавливаем
        btnResult.IsEnabled = true;
        btnResult.Text = "🏆 Показать результат";
    }
}
