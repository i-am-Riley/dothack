#include "pch.h"
#include "CnfData.h"
#include <string>
#include <stdexcept>

const std::string readonly_error = "Object is readonly";

Rileysoft::DotHack::FileFormats::CNF::CnfData::CnfData()
	: m_boot2(), m_ver(), m_vmode(), m_param2(), m_param4(), m_readonly(false)
{
}

const std::string& Rileysoft::DotHack::FileFormats::CNF::CnfData::GetBOOT2() const
{
	return m_boot2;
}

const std::string& Rileysoft::DotHack::FileFormats::CNF::CnfData::GetVER() const
{
	return m_ver;
}

const std::string& Rileysoft::DotHack::FileFormats::CNF::CnfData::GetVMODE() const
{
	return m_vmode;
}

const std::string& Rileysoft::DotHack::FileFormats::CNF::CnfData::GetPARAM2() const
{
	return m_param2;
}

const std::string& Rileysoft::DotHack::FileFormats::CNF::CnfData::GetPARAM4() const
{
	return m_param4;
}

void Rileysoft::DotHack::FileFormats::CNF::CnfData::MakeReadonly()
{
	m_readonly = true;
}

bool Rileysoft::DotHack::FileFormats::CNF::CnfData::GetReadonly()
{
	return m_readonly;
}

void Rileysoft::DotHack::FileFormats::CNF::CnfData::SetBOOT2(const std::string& value)
{
	if (m_readonly)
		throw std::logic_error(readonly_error);
	
	m_boot2 = value;
}

void Rileysoft::DotHack::FileFormats::CNF::CnfData::SetVER(const std::string& value)
{
	if (m_readonly)
		throw std::logic_error(readonly_error);

	m_ver = value;
}

void Rileysoft::DotHack::FileFormats::CNF::CnfData::SetVMODE(const std::string& value)
{
	if (m_readonly)
		throw std::logic_error(readonly_error);

	m_vmode = value;
}

void Rileysoft::DotHack::FileFormats::CNF::CnfData::SetPARAM2(const std::string& value)
{
	if (m_readonly)
		throw std::logic_error(readonly_error);

	m_param2 = value;
}

void Rileysoft::DotHack::FileFormats::CNF::CnfData::SetPARAM4(const std::string& value)
{
	if (m_readonly)
		throw std::logic_error(readonly_error);

	m_param4 = value;
}
