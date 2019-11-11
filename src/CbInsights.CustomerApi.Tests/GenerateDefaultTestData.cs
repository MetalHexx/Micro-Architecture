using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using System;
using System.Collections.Generic;
using System.Text;

namespace CbInsights.CustomerApi.Tests
{
    public class AutoMoqDataAttribute  : AutoDataAttribute
    {
        public AutoMoqDataAttribute (): base(GetDefaultTestFixture)
        {

        }

        public static IFixture GetDefaultTestFixture()
        {
            var autoMoqCustomization = new AutoMoqCustomization();

            return new Fixture().Customize(autoMoqCustomization);
        }
    }
}
