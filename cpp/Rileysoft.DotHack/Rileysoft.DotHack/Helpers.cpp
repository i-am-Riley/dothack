#include "pch.h"
#include "helpers.h"
#include <stdexcept>
#include <string>

std::vector<int> Rileysoft::SplitStr(std::string input, std::string separator)
{
    if (input.empty())
    {
        return std::vector<int>();
    }

    if (separator.empty())
    {
        throw std::invalid_argument("separator must not be empty");
    }

    std::vector<int> positions;
    
    for (int i = 0; i < input.length() - separator.length() + 1; i++)
    {
        bool found = true;

        for (int ii = 0; ii < separator.length(); ii++)
        {
            if (input[i + ii] != separator[ii])
            {
                found = false;
                break;
            }
        }

        if (found)
        {
            positions.push_back(i);
        }
    }

    return positions;
}
