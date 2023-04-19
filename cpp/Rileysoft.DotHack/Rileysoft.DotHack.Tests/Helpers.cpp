#include "pch.h"
#include "CppUnitTest.h"
#include "Helpers.h"
#include <tuple>

using namespace Microsoft::VisualStudio::CppUnitTestFramework;
using namespace Rileysoft;

typedef std::tuple<std::string, std::string, std::vector<int>> SplitStrTestData;

namespace RileysoftDotHackTests
{
	TEST_CLASS(HelpersTests)
	{
	public:
		TEST_METHOD(SplitStr_Input_ReturnsCorrectValue)
		{
			std::vector<SplitStrTestData> tests = {
				{std::string("tester"), std::string("e"), {1, 5}},
				{std::string("a  b  c d"), std::string("  "), {1, 4}}
			};

			for (auto test : tests)
			{
				std::vector<int> output = SplitStr(std::get<0>(test), std::get<1>(test));
				std::vector<int> expected = std::get<2>(test);

				Assert::AreEqual(expected.size(), output.size());
				for (int i = 0; i < expected.size(); i++)
				{
					Assert::AreEqual(expected[0], output[0]);
				}
			}
		}

		TEST_METHOD(SplitStr_EmptyInput_ReturnsEmptyVector)
		{
			std::vector<int> output = SplitStr("", ";");
			Assert::AreEqual(size_t(0), output.size());
		}

		TEST_METHOD(SplitStr_EmptySeparator_Throws)
		{
			Assert::ExpectException<std::invalid_argument>([] {
				SplitStr(" ", "");
			});
		}
	};
}
