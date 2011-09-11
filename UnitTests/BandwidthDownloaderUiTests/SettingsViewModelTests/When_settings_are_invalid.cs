namespace BandwidthDownloaderUiTests.SettingsViewModelTests
{
    using BandwidthDownloaderUi;
    using BandwidthDownloaderUi.Views;

    using FakeItEasy;

    using NUnit.Framework;

    [TestFixture]
    public class When_settings_are_invalid
    {
        private static SettingsViewModel CreateInvalidSettingsViewModel(ISettings fakeSettings)
        {
            const string AnyValue = "a";
            const string AnyInvalidIntValue = "";

            var settingsViewModel = new SettingsViewModel(fakeSettings);

            settingsViewModel.UserName = AnyValue;
            settingsViewModel.Password = AnyValue;
            settingsViewModel.Address = AnyValue;
            settingsViewModel.TimeoutText = AnyInvalidIntValue;

            return settingsViewModel;
        }

        [Test]
        public void Then_it_should_not_be_possible_save_settings()
        {            
            var fakeSettings = A.Fake<ISettings>();
            
            var settingsViewModel = CreateInvalidSettingsViewModel(fakeSettings);

            Assert.IsFalse(settingsViewModel.SaveCommand.CanExecute(null));
        }

        [Test]
        public void And_settings_are_saved_Then_SaveSettings_should_not_be_called()
        {
            var fakeSettings = A.Fake<ISettings>();
            var settingsViewModel = CreateInvalidSettingsViewModel(fakeSettings);

            settingsViewModel.SaveCommand.Execute(null);

            A.CallTo(() => fakeSettings.SaveSettings()).MustNotHaveHappened();
        }
    }
}