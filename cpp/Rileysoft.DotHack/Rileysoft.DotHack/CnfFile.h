#pragma once
#ifdef RILEYSOFTDOTHACK_EXPORTS
#define RILEYSOFTDOTHACK_API __declspec(dllexport)
#else
#define RILEYSOFTDOTHACK_API __declspec(dllimport)
#endif
#include "CnfData.h"
#include <vector>

namespace Rileysoft
{
	namespace DotHack
	{
		namespace FileFormats
		{
			namespace CNF
			{
				/**
				 * @class CnfFile
				 * @brief Performs I/O operations using CnfData.
				 * 
				 * See https://www.psdevwiki.com/ps2/System.cnf
				 */
				class CnfFile
				{
				public:
					RILEYSOFTDOTHACK_API CnfFile();
					RILEYSOFTDOTHACK_API CnfFile(CnfData* cnfData);
					RILEYSOFTDOTHACK_API CnfFile(CnfData* cnfData, bool makeReadonly = false);
					RILEYSOFTDOTHACK_API CnfFile(std::string path, bool makeReadonly = false);
					RILEYSOFTDOTHACK_API bool GetReadonly();
					RILEYSOFTDOTHACK_API void MakeReadonly();
					RILEYSOFTDOTHACK_API CnfData* GetData();
					RILEYSOFTDOTHACK_API void SetData(CnfData* cnfData);
					RILEYSOFTDOTHACK_API std::string Serialize();
					RILEYSOFTDOTHACK_API void SerializeToPath(std::string path);
					RILEYSOFTDOTHACK_API void Deserialize(const char* input);
					RILEYSOFTDOTHACK_API void Deserialize(std::string input);
					RILEYSOFTDOTHACK_API void DeserializeFromPath(std::string path);

				private:
					CnfData* m_data;
				};
			}
		}
	}
}
