namespace BandwidthDownloaderUiTests.AutomatedUITests
{
    using System;

    using NUnit.Framework;

    using White.Core.UIItems;
    using White.Core.UIItems.TabItems;

    [TestFixture]
    public class When_update_button_is_clicked
    {
        [Test]
        [Ignore("UI Automation does not see the elements")]
        public void Then_last_updated_label_should_be_updated()
        {
            using (var appRunner = new ApplicationRunner())
            {
                var button = appRunner.GetControl<Button>("uiaUpdateButton");                
                button.RaiseClickEvent();

                var label = appRunner.GetControl<Label>("uiaLastUpdated");                
                Assert.That(() => label.Text, Is.Not.EqualTo(string.Empty).After(15000, 100));
            }            
        }
    }
}