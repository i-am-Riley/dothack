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
                    code += '            { 0x' + escapeSplit[0] + ', \'\\' + escapeSplit[1].toLowerCase() + '\' }, // ' + escapeSplit[2] + '\n';
                } else {
                    var titleSplit = col.title.split(' - ');
                    code += '            { 0x' + titleSplit[0] + ', \'\\' + titleSplit[1].toLowerCase() + '\' }, // ' + col.innerText + '\n';
                }
            }
            
        }
    }
}

console.log(code);