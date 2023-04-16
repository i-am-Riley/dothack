#pragma once
#ifdef RILEYSOFTDOTHACK_EXPORTS
#define RILEYSOFTDOTHACK_API __declspec(dllexport)
#else
#define RILEYSOFTDOTHACK_API __declspec(dllimport)
#endif

namespace Rileysoft
{
	namespace DotHack
	{
		namespace FileFormats
		{
			namespace CNF
			{
				class CnfFile
				{
				public:
					RILEYSOFTDOTHACK_API void Dummy();
				};
			}
		}
	}
}
