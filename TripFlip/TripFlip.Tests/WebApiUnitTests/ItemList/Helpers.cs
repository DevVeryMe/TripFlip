using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace WebApiUnitTests.ItemList
{
    [TestClass]
    class Helpers
    {
        internal static IEnumerable<object[]> Get_Invalid_ItemList_Title()
        {
            yield return new object[]
            {
                "Test case 1 : Test_ItemList_Title_Validation was given invalid title equals null." +
                " Validation should be failed.",
                null,
                1
            };

            yield return new object[]
            {
                "Test case 2 : Test_ItemList_Title_Validation was given invalid empty Title string." +
                " Validation should be failed.",
                string.Empty,
                1
            };

            yield return new object[]
            {
                "Test case 3 : Test_ItemList_Title_Validation was given invalid title string" +
                " with the length of 101 (which exceeds allowed string length of 100 characters)." +
                " Validation should be failed.",
                new string('*', 101),
                1
            };
        }
    }
}
