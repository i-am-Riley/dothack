#pragma once
#ifdef RILEYSOFTDOTHACK_EXPORTS
#define RILEYSOFTDOTHACK_API __declspec(dllexport)
#else
#define RILEYSOFTDOTHACK_API __declspec(dllimport)
#endif

#include <vector>
#include <string>

namespace Rileysoft
{
	RILEYSOFTDOTHACK_API std::vector<int> SplitStr(std::string input, std::string separator);
}
