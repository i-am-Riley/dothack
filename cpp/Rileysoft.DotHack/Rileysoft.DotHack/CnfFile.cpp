#include "pch.h"
#include "CnfFile.h"
#include "CnfData.h"
#include <string>
#include <stdexcept>
#include <vector>
#include <fstream>
#include <utility>

using namespace Rileysoft::DotHack::FileFormats::CNF;

const std::string readonly_error = "Object is readonly";
const std::string cnfdata_nullptr_error = "cnfData must not be nullptr";

Rileysoft::DotHack::FileFormats::CNF::CnfFile::CnfFile()
{
	m_data = new CnfData();
}

Rileysoft::DotHack::FileFormats::CNF::CnfFile::~CnfFile()
{
	delete m_data;
	m_data = nullptr;
}

Rileysoft::DotHack::FileFormats::CNF::CnfFile::CnfFile(CnfData* cnfData)
{
	if (cnfData == nullptr)
		throw std::invalid_argument(cnfdata_nullptr_error);

	m_data = cnfData;
}

Rileysoft::DotHack::FileFormats::CNF::CnfFile::CnfFile(CnfData* cnfData, bool makeReadonly)
{
	if (cnfData == nullptr)
		throw std::invalid_argument(cnfdata_nullptr_error);

	m_data = cnfData;
	
	if (makeReadonly)
		MakeReadonly();
}

Rileysoft::DotHack::FileFormats::CNF::CnfFile::CnfFile(std::string path, bool makeReadonly)
{
	m_data = new CnfData();
	DeserializeFromPath(path);
	
	if (makeReadonly)
		MakeReadonly();
}

bool Rileysoft::DotHack::FileFormats::CNF::CnfFile::GetReadonly()
{
	if (m_data == nullptr)
		return false;

	return m_data->GetReadonly();
}

void Rileysoft::DotHack::FileFormats::CNF::CnfFile::MakeReadonly()
{
	if (m_data == nullptr)
		return;

	m_data->MakeReadonly();
}

CnfData* Rileysoft::DotHack::FileFormats::CNF::CnfFile::GetData()
{
	return m_data;
}

void Rileysoft::DotHack::FileFormats::CNF::CnfFile::SetData(CnfData* cnfData)
{
	if (cnfData == nullptr)
		throw std::invalid_argument(cnfdata_nullptr_error);

	if (m_data->GetReadonly())
		throw std::logic_error(readonly_error);

	m_data = cnfData;
}

const std::string boot2_str = "BOOT2 = ";
const int boot2_len = 8;
const std::string ver_str = "VER = ";
const int ver_len = 6;
const std::string vmode_str = "VMODE = ";
const int vmode_len = 8;
const std::string param2_str = "PARAM2 = ";
const int param2_len = 9;
const std::string param4_str = "PARAM4 = ";
const int param4_len = 9;

std::string Rileysoft::DotHack::FileFormats::CNF::CnfFile::Serialize()
{
	std::string output = "";

	std::string boot2 = m_data->GetBOOT2(); // convert to copies of the underlying member to prevent mutability of readonly objects.
	std::string ver = m_data->GetVER();
	std::string vmode = m_data->GetVMODE();
	std::string param2 = m_data->GetPARAM2();
	std::string param4 = m_data->GetPARAM4();
	
	if (boot2.length() > 0)
		output += boot2_str + boot2 + "\r\n";

	if (ver.length() > 0)
		output += ver_str + ver + "\r\n";

	if (vmode.length() > 0)
		output += vmode_str + vmode + "\r\n";

	if (param2.length() > 0)
		output += param2_str + param2 + "\r\n";

	if (param4.length() > 0)
		output += param4_str + param4 + "\r\n";

	return output;
}

void Rileysoft::DotHack::FileFormats::CNF::CnfFile::Deserialize(const char* input)
{
	Deserialize(std::string(input));
}

void Rileysoft::DotHack::FileFormats::CNF::CnfFile::Deserialize(std::string input)
{
	if (input.empty())
		throw std::invalid_argument("input must not be empty");

	if (m_data->GetReadonly())
		throw std::logic_error(readonly_error);

	m_data = new CnfData();
	
	size_t i = 0;
	while (i < input.length())
	{
		size_t endOfLine = input.find("\r\n", i);

		// if not found, exit
		if (endOfLine == std::string::npos)
			break;
		
		size_t keyValueSeparator = input.find(" = ", i);

		// not a valid key/value pair
		if (keyValueSeparator == std::string::npos)
		{
			i = endOfLine + 2;
			continue;
		}

		std::string key = input.substr(i, keyValueSeparator - i);
		std::string value = input.substr(keyValueSeparator + 3, endOfLine - keyValueSeparator - 3);

		if (key.length() == 3 && key == "VER")
		{
			m_data->SetVER(value);
		}
		else if (key.length() == 5)
		{
			if (key == "BOOT2")
			{
				m_data->SetBOOT2(value);
			}

			if (key == "VMODE")
			{
				m_data->SetVMODE(value);
			}
		}
		else if (key.length() == 6)
		{
			if (key[0] == 'P' && key[1] == 'A' && key[2] == 'R' && key[3] == 'A' && key[4] == 'M')
			{
				if (key[5] == '2')
				{
					m_data->SetPARAM2(value);
				}
				else if (key[5] == '4')
				{
					m_data->SetPARAM4(value);
				}
			}
		}

		i = endOfLine + 2;
		continue;
	}
}

void Rileysoft::DotHack::FileFormats::CNF::CnfFile::SerializeToPath(std::string path)
{
	std::string output = Serialize();
	std::ofstream outputFile(path);
	outputFile << output;
	outputFile.close();
}

void Rileysoft::DotHack::FileFormats::CNF::CnfFile::DeserializeFromPath(std::string path)
{
	if (m_data == nullptr)
		m_data = new CnfData();

	if (m_data->GetReadonly())
		throw std::logic_error(readonly_error);

	std::ifstream inputFile(path);

	if (inputFile.is_open())
	{
		std::string fileContents((std::istreambuf_iterator<char>(inputFile)),
			std::istreambuf_iterator<char>());
		inputFile.close();

		Deserialize(fileContents);
	}
}
