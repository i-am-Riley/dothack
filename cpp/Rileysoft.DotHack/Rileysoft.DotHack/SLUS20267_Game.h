#pragma once
#ifdef RILEYSOFTDOTHACK_EXPORTS
#define RILEYSOFTDOTHACK_API __declspec(dllexport)
#else
#define RILEYSOFTDOTHACK_API __declspec(dllimport)
#endif

#include <string>
#include "CnfData.h"

namespace Rileysoft
{
	namespace DotHack
	{
		namespace SLUS20267
		{
			class Game
			{
			public:
				RILEYSOFTDOTHACK_API Game();
				RILEYSOFTDOTHACK_API ~Game();
				RILEYSOFTDOTHACK_API const char* GetID();
				RILEYSOFTDOTHACK_API Rileysoft::DotHack::FileFormats::CNF::CnfData* GetData();

			private:
				const char* m_id;
				Rileysoft::DotHack::FileFormats::CNF::CnfData* m_data;
			};
		}
	}
}
