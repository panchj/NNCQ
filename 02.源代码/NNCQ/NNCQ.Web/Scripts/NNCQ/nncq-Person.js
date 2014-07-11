// 将指定的人员数据导入
function importPersonsWithExcel() { }
// 将指定的的人员数据导出
function outportPersonsWithExcel() { }
// 定制的明细页面导航处理
function personDetail(id) {
    $.ajax({
        //cache: true,
        type: 'POST',
        //async: true,
        url: '../../Person/CustomDetail/' + id,
        //dataType: 'json',
        success: function (data) {
            $('#divBoMainArea').html(data);

            //document.getElementById('divBoMainArea').innerHTML = data;
        }
    });
}