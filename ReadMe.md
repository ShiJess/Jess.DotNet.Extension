## Jess.DotNet.Extension


|Package|Nuget|DownLoads|
|:--:|:--:|:--:|
|Jess.DotNet.Extension|[![Jess.DotNet.Extension](https://img.shields.io/nuget/v/Jess.DotNet.Extension.svg)](https://www.nuget.org/packages/Jess.DotNet.Extension/)|[![Jess.DotNet.Extension](https://img.shields.io/nuget/dt/Jess.DotNet.Extension)](https://www.nuget.org/packages/Jess.DotNet.Extension/)|
|Jess.DotNet.ReportViewerExtension.Winforms|[![Jess.DotNet.ReportViewerExtension.Winforms](https://img.shields.io/nuget/v/Jess.DotNet.ReportViewerExtension.Winforms.svg)](https://www.nuget.org/packages/Jess.DotNet.ReportViewerExtension.Winforms/)|[![Jess.DotNet.ReportViewerExtension.Winforms](https://img.shields.io/nuget/dt/Jess.DotNet.ReportViewerExtension.Winforms)](https://www.nuget.org/packages/Jess.DotNet.ReportViewerExtension.Winforms/)|


------------------

# 项目内容介绍

* [ExtensionMethod.md](./docs/ExtensionMethod.md)

## ReportViewer注意事项

* ReportViewer存在内存泄漏问题，故在代码中添加：
``` csharp
    LocalReport.ReleaseSandboxAppDomain();
```
* LocalReport利用了`.NET Remoting`处理报表，其内部释放时间有几分钟，所以**建议在程序起始出添加以下代码**：
``` csharp
    using System.Runtime.Remoting.Lifetime;
```
``` csharp
    //具体时间可依据自身需求调节
    LifetimeServices.LeaseTime = TimeSpan.FromSeconds(5);
    LifetimeServices.LeaseManagerPollTime = TimeSpan.FromSeconds(5);
    LifetimeServices.RenewOnCallTime = TimeSpan.FromSeconds(1);
    LifetimeServices.SponsorshipTimeout = TimeSpan.FromSeconds(5);
```

> [LifetimeServices](https://referencesource.microsoft.com/#mscorlib/system/runtime/remoting/lifetimeservices.cs)

## RoadMap

* TypeConvert扩展库
    * DataTable —— List 互相转换
    * Intptr —— Obj转换
    * 字符串编码转换 —— utf8 ，gb2312【扩展方法】
* String扩展方法处理正则
* 数字转中文&中文大写
* 注册证书生成/读取dll
    * 加解密函数封装
* 图片转ico dll
* MD5验证添加target，自动添加到项目target中执行生成

## 其他参考

``` csharp
using System.Web.Security;
FormsAuthentication.HashPasswordForStoringInConfigFile();
```
