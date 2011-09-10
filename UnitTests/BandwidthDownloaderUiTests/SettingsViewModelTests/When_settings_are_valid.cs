namespace BandwidthDownloaderUiTests.SettingsViewModelTests
{
    using BandwidthDownloaderUi;
    using BandwidthDownloaderUi.Views;

    using FakeItEasy;

    using NUnit.Framework;

    [TestFixture]
    public class When_settings_are_valid
    {
        private static SettingsViewModel CreateValidSettingsViewModel(ISettings fakeSettings)
        {
            const string AnyValue = "a";
            const string AnyValueConvertibleToInt = "0";

            var settingsViewModel = new SettingsViewModel(fakeSettings);

            settingsViewModel.UserName = AnyValue;
            settingsViewModel.Password = AnyValue;
            settingsViewModel.Address = AnyValue;
            settingsViewModel.TimeoutText = AnyValueConvertibleToInt;

            return settingsViewModel;
        }

        [Test]
        public void Then_it_should_be_possible_save_settings()
        {            
            var fakeSettings = A.Fake<ISettings>();
            
            var settingsViewModel = CreateValidSettingsViewModel(fakeSettings);

            Assert.IsTrue(settingsViewModel.SaveCommand.CanExecute(null));
        }

        [Test]
        public void And_settings_are_saved_Then_SaveSettings_should_be_called()
        {
            var fakeSettings = A.Fake<ISettings>();
            var settingsViewModel = CreateValidSettingsViewModel(fakeSettings);

            settingsViewModel.SaveCommand.Execute(null);

            A.CallTo(() => fakeSettings.SaveSettings()).MustHaveHappened(Repeated.Exactly.Once);            
        }
    }
}