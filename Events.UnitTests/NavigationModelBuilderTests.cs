using System;
using events.tac.local.Business.Navigation;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TAC.Utils.TestModels;

namespace Events.UnitTests
{
    [TestClass]
    public class NavigationModelBuilderTests
    {
        [TestMethod]
        public void ReturnAModel()
        {
            var item = new TestItem("TestItem");
            var navigatorBuilder = new NavigationModelBuilder();
            var model = navigatorBuilder.CreateNavigatorMenu(item, item);
            model.Should().NotBeNull();
        }
    }
}
