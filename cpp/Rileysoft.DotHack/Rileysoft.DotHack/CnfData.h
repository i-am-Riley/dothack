#pragma once
#ifdef RILEYSOFTDOTHACK_EXPORTS
#define RILEYSOFTDOTHACK_API __declspec(dllexport)
#else
#define RILEYSOFTDOTHACK_API __declspec(dllimport)
#endif

#include <string>

namespace Rileysoft
{
	namespace DotHack
	{
		namespace FileFormats
		{
			namespace CNF
			{
				/**
				 * @class CnfData
				 * @brief Represents data for a .CNF file.
				 * 
				 * See https://www.psdevwiki.com/ps2/System.cnf for more info.
				 */
				class CnfData
				{
				public:
					RILEYSOFTDOTHACK_API CnfData();
					
					/**
					 * @brief Full path to the executable to launch
					 */
					RILEYSOFTDOTHACK_API const std::string& GetBOOT2() const;

					/**
					 * @brief Title version
					 */
					RILEYSOFTDOTHACK_API const std::string& GetVER() const;

					/**
					 * @brief Video Mode, PAL or NTSC
					 */
					RILEYSOFTDOTHACK_API const std::string& GetVMODE() const;

					/**
					 * @brief Settings for Deckard PS2 model.
					 * 
					 * See https://www.psdevwiki.com/ps2/System.cnf
					 */
					RILEYSOFTDOTHACK_API const std::string& GetPARAM2() const;

					/**
					 * @brief See https://www.psdevwiki.com/ps3/PS2_Emulation#TitleID.2FDiscID_in_ps2_netemu.self
					 */
					RILEYSOFTDOTHACK_API const std::string& GetPARAM4() const;

					/**
					 * @brief Is this object readonly?
					 */
					RILEYSOFTDOTHACK_API bool GetReadonly();

					/**
					 * @brief Sets the BOOT2 value
					 * @param value - New BOOT2 value
					 * @throws std::logic_error Thrown when this property is assigned while the object is readonly.
					 */
					RILEYSOFTDOTHACK_API void SetBOOT2(const std::string& value);

					/**
					 * @brief Sets the VER value
					 * @param value - New VER value
					 * @throws std::logic_error Thrown when this property is assigned while the object is readonly.
					 */
					RILEYSOFTDOTHACK_API void SetVER(const std::string& value);

					/**
					 * @brief Sets the VMODE value
					 * @param value - New VMODE value
					 * @throws std::logic_error Thrown when this property is assigned while the object is readonly.
					 */
					RILEYSOFTDOTHACK_API void SetVMODE(const std::string& value);

					/**
					 * @brief Sets the PARAM2 value
					 * @param value - New PARAM2 value
					 * @throws std::logic_error Thrown when this property is assigned while the object is readonly.
					 */
					RILEYSOFTDOTHACK_API void SetPARAM2(const std::string& value);

					/**
					 * @brief Sets the PARAM4 value
					 * @param value - New PARAM4 value
					 * @throws std::logic_error Thrown when this property is assigned while the object is readonly.
					 */
					RILEYSOFTDOTHACK_API void SetPARAM4(const std::string& value);

					/**
					 * @brief Sets this object as readonly
					 */
					RILEYSOFTDOTHACK_API void MakeReadonly();

				private:
					std::string m_boot2; /// Full path to the executable to launch
					std::string m_ver; /// Title version
					std::string m_vmode; /// Video Mode, PAL or NTSC
					std::string m_param2; /// Settings for Deckard PS2 model.
					std::string m_param4; /// See https://www.psdevwiki.com/ps3/PS2_Emulation#TitleID.2FDiscID_in_ps2_netemu.self
					bool m_readonly; /// Locks this object as readonly
				};
			}
		}
	}
}
