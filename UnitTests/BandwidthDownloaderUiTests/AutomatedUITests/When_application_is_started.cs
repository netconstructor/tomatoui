namespace BandwidthDownloaderUiTests.AutomatedUITests
{
    using System;

    using NUnit.Framework;

    using White.Core.UIItems;
    using White.Core.UIItems.TabItems;

    [TestFixture]
    public class When_application_is_started
    {
        [Test]
        [Ignore("UI Automation does not see the elements")]
        public void Then_last_updated_label_should_be_empty()
        {
            using (var appRunner = new ApplicationRunner())
            {
                var label = appRunner.GetControl<Label>("uiaLastUpdated");
                Assert.AreEqual(string.Empty, label.Text);
            }            
        }
    }
}