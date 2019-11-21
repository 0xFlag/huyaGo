# huyaGo
虎牙直播解析</br>
</br>
编程语言：C#</br>
编程软件：Visual Studio 2012</br>
编程环境：.Net Framework 4.5</br>
Version版本: 1.0 未完成</br>
外部调用DLL库: Newtonsoft.Json.dll</br>
</br>
目前只完成了tabContro2里的单个直播间解析直播源功能</br>
显示主播头像和实时截图（需要重复解析）</br>
直播源分为:</br>
1.v3.2_19082301 / v3.2_18011202</br>
http://hyplayer.msstatic.com/v3.2_19082301/main.swf?topSid=" + channel + "&subSid=" + liveChannel + "&pnick=" + nick</br>
v3.2_19092602</br>
http://hyplayer.msstatic.com/v3.2_19092602/main.swf?topSid=" + channel + "&subSid=" + liveChannel + "&pyyid=" + yyid</br>
</br>
flv</br>
http://al.flv.huya.com/backsrc/ + "sStreamName".flv</br>
http://al.flv.huya.com/huyalive/ + "sStreamName".flv</br>
http://tx.flv.huya.com/backsrc/ + "sStreamName".flv</br>
http://tx.flv.huya.com/huyalive/ + "sStreamName".flv</br>
http://js.flv.huya.com/backsrc/ + "sStreamName".flv</br>
http://js.flv.huya.com/huyalive/ + "sStreamName".flv</br>
m3u8</br>
http://al.flv.huya.com/backsrc/ + "sStreamName".m3u8</br>
http://al.flv.huya.com/huyalive/ + "sStreamName".m3u8</br>
http://al.hls.huya.com/backsrc/ + "sStreamName".m3u8</br>
http://al.hls.huya.com/huyalive/ + "sStreamName".m3u8</br>
http://tx.flv.huya.com/backsrc/ + "sStreamName".m3u8</br>
http://tx.flv.huya.com/huyalive/ + "sStreamName".m3u8</br>
http://tx.hls.huya.com/backsrc/ + "sStreamName".m3u8</br>
http://tx.hls.huya.com/huyalive/ + "sStreamName".m3u8</br>
http://js.flv.huya.com/backsrc/ + "sStreamName".m3u8</br>
http://js.flv.huya.com/huyalive/ + "sStreamName".m3u8</br>
http://js.hls.huya.com/backsrc/ + "sStreamName".m3u8</br>
http://js.hls.huya.com/huyalive/ + "sStreamName".m3u8</br>
slice
http://al.p2p.huya.com/huyalive/ + "sStreamName" + _0_2_66.slice</br>
http://tx.p2p.huya.com/huyalive/ + "sStreamName" + _0_2_66.slice</br>
http://js.p2p.huya.com/huyalive/ + "sStreamName" + _0_2_66.slice</br>
直播格式为:</br>
flash 默认 1500 1200 800 500 100</br>
</br>
tabContro3</br>
完成: </br>
1.直播分类获取</br>
2.直播分类数量</br>
3.分类网址获取</br>
未完成:</br>
1.直播分类封面</br>
2.直播分类内所有在线主播</br>
3.ListView功能</br>
</br>
未解决:</br>
正则太长获取会导致程序未响应
