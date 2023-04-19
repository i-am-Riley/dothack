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
	m_data = nullptr;
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

	std::vector<int> newlines;

	for (int i = 0; i < input.length() - 1; i++)
	{
		if (input[i] == '\r' && input[i + 1] == '\n')
		{
			newlines.push_back(i);
		}
	}

	for (int i = 0; i < newlines.size(); i++)
	{
		int start = 0;
		
		if (i > 0)
		{
			start = newlines[i - 1]+2;
		}

		int end = newlines[i] - 1;
		int length = end - start;

		if (length > ver_len)
		{
			if (input.substr(start, ver_len).compare(ver_str) == 0)
			{
				m_data->SetVER(input.substr(start + ver_len + 1, end));
				continue;
			}
		}

		if (length > boot2_len)
		{
			if (input.substr(start, boot2_len).compare(boot2_str) == 0)
			{
				m_data->SetBOOT2(input.substr(start + boot2_len + 1, end));
				continue;
			}

			if (input.substr(start, vmode_len).compare(vmode_str) == 0)
			{
				m_data->SetVMODE(input.substr(start + vmode_len + 1, end));
				continue;
			}
		}

		if (length > param2_len)
		{
			if (input.substr(start, param2_len).compare(param2_str) == 0)
			{
				m_data->SetPARAM2(input.substr(start + param2_len + 1, end));
				continue;
			}

			if (input.substr(start, param4_len).compare(param4_str) == 0)
			{
				m_data->SetPARAM4(input.substr(start + param4_len + 1, end));
			}
		}
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
