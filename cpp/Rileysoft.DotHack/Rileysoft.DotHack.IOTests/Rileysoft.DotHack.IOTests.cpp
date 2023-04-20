// Rileysoft.DotHack.IOTests.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include <iostream>
#include "CnfFile.h"
#include <Windows.h>
#include <Shlwapi.h>

using Rileysoft::DotHack::FileFormats::CNF::CnfFile;

int main()
{
    std::cout << "IO Tests\nTest 1 - system.cnf write/read\n\n";

    const char* systemCnf = "system.cnf";

    if (PathFileExistsA(systemCnf))
    {
        if (DeleteFileA(systemCnf))
        {
            std::cout << "Deleted old system.cnf file\n";
        }
        else
        {
            std::cout << "Failed to delete old system.cnf file\n";
        }
    }

    CnfFile* cnfFile = new CnfFile();
    cnfFile->GetData()->SetBOOT2("Test");
    cnfFile->GetData()->SetVER("1.0");
    cnfFile->GetData()->SetVMODE("NTSC");

    cnfFile->SerializeToPath(systemCnf);

    if (!PathFileExistsA(systemCnf))
    {
        std::cout << "Did not find system.cnf!\n";
    }

    CnfFile* cnfFileRead = new CnfFile(systemCnf, true);
    if (cnfFileRead->GetData()->GetBOOT2() == "Test" &&
        cnfFileRead->GetData()->GetVER() == "1.0" &&
        cnfFileRead->GetData()->GetVMODE() == "NTSC" &&
        cnfFileRead->GetData()->GetPARAM2().empty() &&
        cnfFileRead->GetData()->GetPARAM4().empty())
    {
        std::cout << "OK!\n";
    }
    else
    {
        std::cout << "Mismatched system.cnf\n";
    }

    std::cout << "\n";
}

// Run program: Ctrl + F5 or Debug > Start Without Debugging menu
// Debug program: F5 or Debug > Start Debugging menu

// Tips for Getting Started: 
//   1. Use the Solution Explorer window to add/manage files
//   2. Use the Team Explorer window to connect to source control
//   3. Use the Output window to see build output and other messages
//   4. Use the Error List window to view errors
//   5. Go to Project > Add New Item to create new code files, or Project > Add Existing Item to add existing code files to the project
//   6. In the future, to open this project again, go to File > Open > Project and select the .sln file
