<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.CPViewControl"%>


<div id="toolbar-box">
    <div class="t"><div class="t"><div class="t"></div></div></div>
    <div class="m">
        <div class="toolbar-list" id="toolbar">
            
        </div>
        <div class="pagetitle icon-48-generic">
            <h2>File tải lên</h2>
        </div>
        <div class="clr"></div>
    </div>
    <div class="b"><div class="b"> <div class="b"></div></div></div>
</div>
<div class="clr"></div>

<div id="element-box">
    <div class="t"><div class="t"><div class="t"></div></div></div>
    <div class="m">

        <p style="padding-left: 0px; padding-right: 0px;">
		<script type="text/javascript">

		    // This is a sample function which is called when a file is selected in CKFinder.
		    function showFileInfo(fileUrl, data) {
		        var msg = 'The selected URL is: <a href="' + fileUrl + '">' + fileUrl + '</a><br /><br />';
		        // Display additional information available in the "data" object.
		        // For example, the size of a file (in KB) is available in the data["fileSize"] variable.
		        if (fileUrl != data['fileUrl'])
		            msg += '<b>File url:</b> ' + data['fileUrl'] + '<br />';
		        msg += '<b>File size:</b> ' + data['fileSize'] + 'KB<br />';
		        msg += '<b>Last modified:</b> ' + data['fileDate'];

		        // this = CKFinderAPI object
		        this.openMsgDialog("Selected file", msg);
		    }

		    // You can use the "CKFinder" class to render CKFinder in a page:
		    var finder = new CKFinder();
		    // The path for the installation of CKFinder (default = "/ckfinder/").
		    finder.basePath = '../';
		    // The default height is 400.
		    finder.height = 600;
		    // This is a sample function which is called when a file is selected in CKFinder.
		    finder.selectActionFunction = showFileInfo;
		    finder.create();

		    // It can also be done in a single line, calling the "static"
		    // create( basePath, width, height, selectActionFunction ) function:
		    // CKFinder.create( '../', null, null, showFileInfo );

		    // The "create" function can also accept an object as the only argument.
		    // CKFinder.create( { basePath : '../', selectActionFunction : showFileInfo } );

		  </script>
	    </p>
             

        <div class="clr"></div>
    </div>
    <div class="b"><div class="b"><div class="b"></div></div></div>
</div>
