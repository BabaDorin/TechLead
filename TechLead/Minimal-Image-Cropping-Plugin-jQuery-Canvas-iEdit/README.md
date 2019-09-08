<h1>iEdit.</h1>

<p>
iEdit is a JavaScript library which allows you to add an image cropping and modifying interface to your website. You no longer need to create advanced image processing scripts in WebGL; all you need to do is provide iEdit with the image files and deal with the image modified by the user. No need to make your own interface or hassle around with aspect ratios, resolutions and file types; iEdit has you covered. Just include the script and the style files and call the functions. That is it.
</p>

<h2>Usage</h2>
To use iEdit, just include iEdit's script file and the style file. <b>jQuery</b> is the only dependancy and must included as well.

<h3>API</h3>

<ul>
<li>
<h4><code>iEdit.open()</code></h4>
Opens the image editor with the provided image object. The image modified by the user it supplied to the callback as a DataURI. Additional settings are supplied as parameters to <code>iEdit.open()</code>.

<h3>Syntax and Parameter</h3>
<code>iEdit.open(imageObject, square, callback, imageType, imageQuality)</code>
<ul>
<li>
<b><code>imageObject</code></b><br>
Required - A File object of the image to be edited. The File should be a valid image.
</li><li>
<b><code>square</code></b><br>
Optional - Boolean value specifying if the image cropped by the user should only be a square. If <code>true</code>, users can only can crop the image to be a square.
<br>
<b>Default:</b><code>false</code>
</li><li>
<b><code>callback</code></b><br>
Optional - A function to use the image cropped by the user. The image cropped by the user is supplied to the function as the first parameter.
</li><li>
<b><code>imageType</code></b><br>
Optional - A String specifying the type of the image returned to the callback function. Possible values are: "jpeg", "png", "gif" ,"bmp".
<br>
<b>Default:</b><code>"jpeg"</code>
</li><li>
<b><code>imageQuality</code></b><br>
Optional - A Number greater than 0 and less that or equal to 1 specifying the quality of the image returned to the callback function. 
<br>
<b>Default:</b><code>1</code>
</li>
</ul>

<h3>Return Values</h3>
Returns <code>true</code> if the image supplied is valid. Returns <code>false</code> if the image supplied is not a valid image.
The return can be used to check if the image supplied is valid and take appropriate actions.

<h3>Example</h3>
<pre>
<code>
$(document).ready(function(){<br>
	$("input[type=file]").change(function(e){<br>
		var img = e.target.files[0];<br>
		if( !iEdit.open(img, true, function(res){<br>
			$("img#result").attr("src", res);<br>
		}, "png", 0.85) ){<br>
			alert("The image provided is not valid!");<br>
		}<br>
	});<br>
});<br>
</code>
</pre>
</li>
<li>
<h4><code>iEdit.close()</code></h4>
Closes the image editior without calling the callback supplied to <code>iEdit.open()</code>. <code>iEdit.close()</code> is called when the user clicks on the cancel button in the image editor.

<h3>Syntax</h3>
<code>iEdit.close()</code>

</li>	
<li>
<h4><code>iEdit.status</code></h4>
Contains a boolean value specifying if the image editor is open or not. If <code>true</code>, the image editor is open, else it is not.
</li>
</ul>