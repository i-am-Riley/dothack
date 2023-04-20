
#include "pch.h"
#include "SLUS20267_Game.h"
#include "CnfData.h"

Rileysoft::DotHack::SLUS20267::Game::Game() : m_id("SLUS-20267")
{
	m_data = new Rileysoft::DotHack::FileFormats::CNF::CnfData();
	m_data->SetBOOT2("cdrom0:\\SLUS_202.67;1");
	m_data->SetVER("1.00");
	m_data->SetVMODE("NTSC");
	m_data->MakeReadonly();
}

Rileysoft::DotHack::SLUS20267::Game::~Game()
{
	delete m_data;
	m_data = nullptr;
}

const char* Rileysoft::DotHack::SLUS20267::Game::GetID()
{
	return m_id;
}

Rileysoft::DotHack::FileFormats::CNF::CnfData* Rileysoft::DotHack::SLUS20267::Game::GetData()
{
	return m_data;
}
