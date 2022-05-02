$(document).ready(function () {
    VisibleGeneralTable();
});

function SelectedRow() {
    $('#RowCount').append(
        '<option>5</option>' +
        '<option>10</option>' +
        '<option>20</option>'
    );
}

var GetMaxRow = 0;
var GetLastRow = 0;
var GetFirstRow = 0;
var AllCount = 0;
var PageCount = 1;
function GetFirstPage() {
    $('.SelectTableGenereal').detach();
    GetMaxRow = $('#RowCount option:selected').val();
    SelectGeneralTable(0, GetMaxRow);
    $('#refCurrent').text(1);
    PageCount = 1;
}

function GetNextPage() {
    var topmin = Number(GetLastRow + 1);
    var topmax = Number(topmin) + Number($('#RowCount option:selected').prop('value'));
    if (AllCount > GetLastRow)
    {
        PageCount++;
        $('.SelectTableGenereal').detach();
        SelectGeneralTable(topmin, topmax-1);
        $('#refCurrent').text(PageCount);
    }
}

function GetPrePage() {
    var topmax = Number(GetFirstRow);
    var topmin = Number(topmax) - Number($('#RowCount option:selected').prop('value'));
    if (topmin > 0) {
        PageCount--;
        $('.SelectTableGenereal').detach();
        SelectGeneralTable(topmin, (topmax - 1));
        $('#refCurrent').text(PageCount);
    }
    else {
        $('.SelectTableGenereal').detach();
        SelectGeneralTable(0, Number($('#RowCount option:selected').prop('value')));
    }
}

function GetLastPage() {
    var e = AllCount % Number($('#RowCount option:selected').prop('value'));
    var topmin = AllCount - e + 1;
    var topmax = AllCount;
        $('.SelectTableGenereal').detach();
        SelectGeneralTable(topmin, topmax);
        $('#refCurrent').text(e - 1);

}


function AddObject() {
    $('#BlockObjects').append(
        '<div class="SingleObject divBlock">' +
            '<div class="col-sm-12 row">' + 
                '<span class= "col-sm-4">Код объекта: </span>' + 
                '<input type="text" class="form-control codeBlock col-sm-6">' + 
            '</div>' + 
            '<div class="col-sm-12 row">' + 
                '<span class="col-sm-4">значение объекта: </span>' + 
                '<input type="text" class="form-control descriptionBlock col-sm-6">' + 
            '</div>' + 
        '</div>'
    );
}


function SaveAllObjects() {
    var blocks = $('.SingleObject');
    var model = new Array();

    blocks.each(function (i, v) {
        var code = $(this).find('.codeBlock').prop('value');
        var desc = $(this).find('.descriptionBlock').prop('value');
        var section = { 'Code': code, 'Description': desc, 'RowNumber': i };

        if (code == '' || desc == '') {
            alert('Не все поля заполнены');
            return false;
        }

        model.push(section);
    });

    $.ajax({
        type: "POST",
        url: window.location.origin + "/Home/SaveAllObjects",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        Accept: "application/json",
        dataType: "json",
        async: false,
        success: function (data) {
            GetFirstPage();
            GetGeneralCount();
        }
    });
}


function GetGeneralCount() {
    $.ajax({
        type: "POST",
        url: window.location.origin + "/Home/GetGeneralCount",
        data: JSON.stringify(),
        contentType: "application/json; charset=utf-8",
        Accept: "application/json",
        dataType: "json",
        async: false,
        success: function (data) {
            $('#GeneralCnt').append(data);
            AllCount = Number(data);
        }
    });
}



function SelectGeneralTable(tm, tx) {
    var obj = new Object();
    obj.topmin = Number(tm);
    obj.topmax = Number(tx);

    $.ajax({
        type: "POST",
        url: window.location.origin + "/Home/SelectGeneralTable",
        data: JSON.stringify(obj),
        contentType: "application/json; charset=utf-8",
        Accept: "application/json",
        dataType: "json",
        async: false,
        success: function (data) {
            $.each(data, function (i, v) {
                $('#GenerealTable').append(
                    "<tr class='SelectTableGenereal' value=" + this.id + ">"
                    + "<td>" + this.cntNumber + "</td>"
                    + "<td>" + this.code + "</td>"
                    + "<td>" + this.description + "</td>"
                    + "</tr>"
                );
            });
            GetLastRow = Number($('#GenerealTable tr:last td:eq(0)').text());
            GetFirstRow = Number($('#GenerealTable tr:eq(1) td:eq(0)').text());
        }
    });
}


function GetGeneralTable() {
    var topmin = Number($('#GenerealTable tr:last td:eq(0)').text()) + 1;
    var topmax = topmin + $('#RowCount option:selected').prop('value');
    SelectGeneralTable(topmin, topmax);
}


function VisibleGeneralTable() {
    $.post(window.location.origin + "/Home/_GetTable",
        function (response) {
            $("#divGetTable").html(response);
            SelectedRow();
            GetGeneralCount();
            SelectGeneralTable(0, $('#RowCount option:selected').prop('value'));            
        });
}

function VisibleNewObjects() {
    $.post(window.location.origin + "/Home/_GetNewObject",
        function (response) {
            $("#divGetNewObj").html(response);
            $("#divGetNewObj").show('slow');
        });
}