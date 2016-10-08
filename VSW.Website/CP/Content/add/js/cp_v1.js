function vsw_exec_cmd(cmd_name) {
    cmd_name = cmd_name.replace('-','');
    if (cmd_name) {
        var cmd_param = '';
        if (cmd_name == "copy") {
            var list_cid = document.getElementsByName('cid');
            for (var i = 0; i < list_cid.length; i++) {
                if (list_cid[i].checked) {
                    cmd_param = list_cid[i].value;
                    break;
                }
            }
        }
        else if (cmd_name == "publish" || cmd_name == "unpublish" || cmd_name == 'delete') {
            var list_cid = document.getElementsByName('cid');
            for (var i = 0; i < list_cid.length; i++) {
                if (list_cid[i].checked) {
                    cmd_param += (cmd_param == '' ? '' : ',') + list_cid[i].value;
                }
            }
        }
        else if (cmd_name == "edit") {
            var list_cid = document.getElementsByName('cid');
            for (var i = 0; i < list_cid.length; i++) {
                if (list_cid[i].checked) {
                    cmd_param = list_cid[i].value;
                    break;
                }
            }
            VSWRedirect('Add', cmd_param, 'RecordID');
            return;
        }
        else if (cmd_name == "saveorder") {
            var list_cid = document.getElementsByName('cid');
            for (var i = 0; i < list_cid.length; i++) {
                cmd_param += (cmd_param == '' ? '' : ',') + list_cid[i].value;
                var order = document.getElementById('order[' + list_cid[i].value + ']');
                cmd_param += (cmd_param == '' ? '' : ',') + order.value;
            }
        }

        if (cmd_param != '')
            cmd_name = '[' + cmd_name + '][' + cmd_param + ']';

        document.getElementById('_vsw_action').value = cmd_name;
    }
    if (typeof document.vswForm.onsubmit == "function") {
        document.vswForm.onsubmit();
    }
    document.vswForm.submit();
}

function isChecked(isitchecked) {
    if (isitchecked == true) {
        document.vswForm.boxchecked.value++;
    }
    else {
        document.vswForm.boxchecked.value--;
    }
}

function checkAll(n, fldName) {
    if (!fldName) {
        fldName = 'cb';
    }
    var f = document.vswForm;
    var c = f.toggle.checked;
    var n2 = 0;
    for (i = 0; i < n; i++) {
        cb = eval('f.' + fldName + '' + i);
        if (cb) {
            cb.checked = c;
            n2++;
        }
    }
    if (c) {
        document.vswForm.boxchecked.value = n2;
    } else {
        document.vswForm.boxchecked.value = 0;
    }
}

function gmobj(o) {
    if (document.getElementById) { m = document.getElementById(o); }
    else if (document.all) { m = document.all[o]; }
    else if (document.layers) { m = document[o]; }
    return m;
}

function getNodeValue(o) {
    try {
        return o.item(0).firstChild.nodeValue;
    }
    catch (err) {
        return '';
    }
}

function VSWCheckDefaultValue(value, name) {
    if (typeof (VSWArrDefault) != 'undefined') {
        for (var i = 0; i < VSWArrDefault.length; i++) {
            if (i == VSWArrDefault.length - 1) break;

            if (VSWArrDefault[i] == value && VSWArrDefault[i + 1] == name)
                return true;

            i++;
        }
    }

    return false;
}

function VSWRedirect(control, value, name) {
    var sURL = '';

    if (value && value != '' && value != '0')
        sURL += '/' + (name ? name : 'RecordID') + '/' + value;

    if (typeof (VSWArrVar) != 'undefined') {
        for (var i = 0; i < VSWArrVar.length; i++) {
            if (i == VSWArrVar.length - 1) break;

            var obj = document.getElementById(VSWArrVar[i]);
            if (obj != null) {
                var _value = obj.value;
                if (_value != '' && _value != '0') {
                    if (!VSWCheckDefaultValue(_value, VSWArrVar[i + 1])) 
                      sURL += '/' + VSWArrVar[i + 1] + '/' + _value;
                }
            }

            i++;
        }
    }

    if (typeof (VSWArrQT) != 'undefined') {
        for (var i = 0; i < VSWArrQT.length; i++) {
            if (i == VSWArrQT.length - 1) break;

            if (name && name == VSWArrQT[i + 1]) {
                i++;
                continue;
            }

            if ((control ? control : 'Index') == 'Index' && 'PageIndex' == VSWArrQT[i + 1]) {
                i++;
                continue;
            }

            if (VSWArrQT[i] != '' && VSWArrQT[i] != '0')
                if (!VSWCheckDefaultValue(VSWArrQT[i], VSWArrQT[i + 1]))
                   sURL += '/' + VSWArrQT[i + 1] + '/' + VSWArrQT[i];

            i++;
        }
    }

    if (typeof (VSWArrVar_QS) != 'undefined') {
        var _url = '';
        for (var i = 0; i < VSWArrVar_QS.length; i++) {
            if (i == VSWArrVar_QS.length - 1) break;

            var obj = document.getElementById(VSWArrVar_QS[i]);
            if (obj != null) {
                var _value = obj.value;
                if (_value != '' && _value != '0') {
                    if (!VSWCheckDefaultValue(_value, VSWArrVar_QS[i + 1]))
                        _url += ( _url == '' ? '' : '&' ) + VSWArrVar_QS[i + 1] + '=' + _value;
                }
            }

            i++;
        }
        if (_url != '') 
          sURL = sURL + '?' + _url;
    }

    if (typeof (VSWArrQT_QS) != 'undefined') {
        var _url = '';
        for (var i = 0; i < VSWArrQT_QS.length; i++) {
            if (i == VSWArrQT_QS.length - 1) break;

            if (VSWArrQT_QS[i] != '' && VSWArrQT_QS[i] != '0')
                if (!VSWCheckDefaultValue(VSWArrQT_QS[i], VSWArrQT_QS[i + 1]))
                    _url += (_url == '' ? '' : '&') + VSWArrQT_QS[i + 1] + '=' + VSWArrQT_QS[i];

            i++;
        } 
        if (_url != '')
           sURL = sURL + '?' + _url;
    }

    if (control)
        sURL = control + '.aspx' + sURL;
    else
        sURL = 'Index.aspx' + sURL;

    window.location.href = '/' + CPPath + '/' + VSWController + '/' + sURL; 
}

function RedirectController(ControllerName, control, value, name) {
    var sURL = '';

    if (value && value != '' && value != '0')
        sURL += '/' + (name ? name : 'RecordID') + '/' + value;

    if (typeof (VSWArrVar) != 'undefined') {
        for (var i = 0; i < VSWArrVar.length; i++) {
            if (i == VSWArrVar.length - 1) break;

            var obj = document.getElementById(VSWArrVar[i]);
            if (obj != null) {
                var _value = obj.value;
                if (_value != '' && _value != '0') {
                    if (!VSWCheckDefaultValue(_value, VSWArrVar[i + 1]))
                        sURL += '/' + VSWArrVar[i + 1] + '/' + _value;
                }
            }

            i++;
        }
    }

    if (typeof (VSWArrQT) != 'undefined') {
        for (var i = 0; i < VSWArrQT.length; i++) {
            if (i == VSWArrQT.length - 1) break;

            if (name && name == VSWArrQT[i + 1]) {
                i++;
                continue;
            }

            if ((control ? control : 'Index') == 'Index' && 'PageIndex' == VSWArrQT[i + 1]) {
                i++;
                continue;
            }

            if (VSWArrQT[i] != '' && VSWArrQT[i] != '0')
                if (!VSWCheckDefaultValue(VSWArrQT[i], VSWArrQT[i + 1]))
                    sURL += '/' + VSWArrQT[i + 1] + '/' + VSWArrQT[i];

            i++;
        }
    }

    if (typeof (VSWArrVar_QS) != 'undefined') {
        var _url = '';
        for (var i = 0; i < VSWArrVar_QS.length; i++) {
            if (i == VSWArrVar_QS.length - 1) break;

            var obj = document.getElementById(VSWArrVar_QS[i]);
            if (obj != null) {
                var _value = obj.value;
                if (_value != '' && _value != '0') {
                    if (!VSWCheckDefaultValue(_value, VSWArrVar_QS[i + 1]))
                        _url += (_url == '' ? '' : '&') + VSWArrVar_QS[i + 1] + '=' + _value;
                }
            }

            i++;
        }
        if (_url != '')
            sURL = sURL + '?' + _url;
    }

    if (typeof (VSWArrQT_QS) != 'undefined') {
        var _url = '';
        for (var i = 0; i < VSWArrQT_QS.length; i++) {
            if (i == VSWArrQT_QS.length - 1) break;

            if (VSWArrQT_QS[i] != '' && VSWArrQT_QS[i] != '0')
                if (!VSWCheckDefaultValue(VSWArrQT_QS[i], VSWArrQT_QS[i + 1]))
                    _url += (_url == '' ? '' : '&') + VSWArrQT_QS[i + 1] + '=' + VSWArrQT_QS[i];

            i++;
        }
        if (_url != '')
            sURL = sURL + '?' + _url;
    }

    if (control)
        sURL = control + '.aspx' + sURL;
    else
        sURL = 'Index.aspx' + sURL;

    window.location.href = '/' + CPPath + '/' + ControllerName + '/' + sURL;
}

function RedirectControllerParameter(ControllerName, control, Parameters) {
    var sURL = '';
    Parameters
    if (control)
        sURL = control + '.aspx' + sURL;
    else
        sURL = 'Index.aspx' + sURL;

    window.location.href = '/' + CPPath + '/' + ControllerName + '/' + control + '/' + Parameters;
}

function trim(str, chars) {
    return ltrim(rtrim(str, chars), chars);
}

function ltrim(str, chars) {
    chars = chars || "\\s";
    return str.replace(new RegExp("^[" + chars + "]+", "g"), "");
}

function rtrim(str, chars) {
    chars = chars || "\\s";
    return str.replace(new RegExp("[" + chars + "]+$", "g"), "");
}


function GetIndex(custom, key, index) {
    var i = custom.indexOf(key + '=', index);
    if (i > -1) {
        var k = custom.indexOf('\n', i - 1); 
        if (k == -1 || k == i - 1 || k == custom.length - 1) {
            return i;
        }
        else {
            if (k < i) {
                var s = custom.substr(k, i - k);
                s = trim(s, '');

                if (s == '')
                    return i;
                else
                    return GetIndex(custom, key, i + key.length + 1);
            }

            return i;
        }
    }

    return -1;
}

function getvalue(custom, key, value) {
    return getvalue(custom, key, value, 0);
}

function getvalue(custom, key, value, index) {
    var i = GetIndex(custom, key, 0);
    if (i > -1) {
        var j = custom.indexOf('\n', i);
        if (j == -1) j = custom.length;

        var oldvalue = custom.substr(i, j - i);

        custom = custom.replace(oldvalue, key + '=' + value);
    }
    else {
        if (custom == '') custom = key + '=' + value;
        else custom += '\n' + key + '=' + value;
    }

    return custom;
}

function GetCustom(key) {
    var txtCustom = document.getElementById("custom");
    var txtSetCustom = document.getElementById("set_custom");

    var custom = txtCustom.value;
    txtSetCustom.value = '';

    var i = GetIndex(custom, key, 0);
    if (i > -1) {
        var j = custom.indexOf('\n', i);

        if (j == -1)
            j = custom.length;

        var value = custom.substr(i + key.length + 1, j - i - key.length - 1);

        txtSetCustom.value = value;
    }
}

function SetCustom() {
    var key = '';
    for (var i = 0; i < document.getElementsByName("rSetCustom").length; i++) {
        if (document.getElementsByName("rSetCustom").item(i).checked) {
            key = document.getElementsByName("rSetCustom").item(i).value;
            break;
        }
    }

    var txtCustom = document.getElementById("custom");
    var txtSetCustom = document.getElementById("set_custom");
    var sCode = '';

    if (txtCustom.value != '')
        sCode = txtCustom.value;

    sCode = getvalue(sCode, key, txtSetCustom.value);

    txtCustom.value = sCode;
}


//update custom - page
function UpdateCustom(cID, sType) {
    var key = cID.toString().replace("_", ".") + '';
    var value = document.getElementById(cID).value + '';

    var txtCustom = document.getElementById("custom");
    var sCode = '';

    if (txtCustom.value != '')
        sCode = txtCustom.value;

    sCode = getvalue(sCode, key, value);

    txtCustom.value = sCode;
}


function vsw_checkAll(form, field, value) {
    for (i = 0; i < form.elements.length; i++) {
        if (form.elements[i].name == field) {
            form.elements[i].checked = value;
            if (form.elements[i].disabled)
                form.elements[i].checked = false;
        }
    }
}

function ShowNewsForm(cID, sValue) {
    name_control = cID;
    window.open("/" + CPPath + "/FormNews/Index.aspx?Value=" + sValue, "", "width=1024, height=800, top=80, left=200,scrollbars=yes");
    return false;
}

function ShowTextForm(cID, sValue) {
    name_control = cID;
    window.open("/" + CPPath + "/FormText/Index.aspx?TextID=" + cID, "", "width=1024, height=800, top=80, left=200,scrollbars=yes");
    return false;
}

function ShowFileForm(cID, sValue) {
    name_control = cID;

    var finder = new CKFinder();
    finder.basePath = '../'; 
    finder.selectActionFunction = refreshPage;
    finder.popup();

    return false;
}

var name_control = '';
function refreshPage(arg) {
    var obj = document.getElementById(name_control);
    if (name_control.indexOf('File') > -1 || name_control.indexOf('Img') > -1 || name_control.indexOf('Logo') > -1)
        obj.value = '~' + arg;
    else
        obj.value = arg;
}

function layout_change(pid, list_param, layout) {
    if (list_param == '') return;
    var listLayout = list_param.split(',')
    for (var i = 0; i < listLayout.length; i++) {
        var ib = listLayout[i].indexOf('[');
        //var ie = listLayout[i].indexOf(']');
        var _layout = listLayout[i].substring(0, ib);
        var _list_control_param = listLayout[i].substring(ib + 1, listLayout[i].length - 1);

        if (_layout == 'Default' || _layout == layout)
            control_change(pid, _list_control_param);
    }
}
function control_change(pid, list_param) {
    var list_control = list_param.split('|');
    for (var i = 0; i < list_control.length; i++) {
        var control = list_control[i].split('-')[0];
        var visible = list_control[i].split('-')[1];
        //document.getElementById(pid + '_' + control).disabled = (visible == 'false');
        document.getElementById('tr_' + pid + '_' + control).style.display = (visible == 'false' ? 'none' : '');
    }
}

function control_set_value(id, value) {
    var obj = document.getElementById(id);
    if (obj) {
        obj.value = value;
    } else {
        if (value == 'True') value = 1;
        if (value == 'False') value = 0;
        var arr = document.getElementsByName(id);
        if (arr != null) {
            for (var j = 0; j < arr.length; j++) {
                if (arr[j].value == value) {
                    arr[j].checked = true;
                    break;
                }
            }
        }

    }
}