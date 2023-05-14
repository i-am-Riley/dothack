// for https://uic.io/en/charset/show/shift_jis/
var code = "";

var rows = $('table[class*="charset"]').find('tr');
for (var i = 0; i < rows.length; i++) {
    if (rows[i].children.length === 17 && (!rows[i].classList.contains("index"))) {
        var $row = $(rows[i]);
        var $cols = $row.find('td');
        var headerCol = $cols[0];
        code += '            // ' + headerCol.innerText + '\n';

        for (var y = 1; y < $cols.length; y++) {
            var col = $cols[y];
            if (!col.classList.contains('notdefined')) {
                if (col.classList.contains('escape')) {
                    var escapeSplit = col.title.split(' - ');
                    var hexNum = escapeSplit[0];
                    var val = 'new byte[] { ';
                    if (hexNum.length == 2) {
                        val += '0x' + hexNum + ' } ';
                    } else {
                        val += '0x' + hexNum.substring(0, 2) + ', 0x' + hexNum.substring(2) + ' } ';
                    }

                    code += '            { \'\\' + escapeSplit[1].toLowerCase() + '\', ' + val + '  }, // ' + escapeSplit[2] + '\n';
                } else {
                    var titleSplit = col.title.split(' - ');
                    var hexNum = titleSplit[0];
                    var val = 'new byte[] { ';
                    if (hexNum.length == 2) {
                        val += '0x' + hexNum + ' } ';
                    } else {
                        val += '0x' + hexNum.substring(0, 2) + ', 0x' + hexNum.substring(2) + ' } ';
                    }

                    code += '            { \'\\' + titleSplit[1].toLowerCase() + '\', ' + val + ' }, // ' + col.innerText + '\n';
                }
            }

        }
    }
}

console.log(code);