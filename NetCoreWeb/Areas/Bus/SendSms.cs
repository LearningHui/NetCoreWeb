using Aliyun.Acs.Core;
using Aliyun.Acs.Core.Exceptions;
using Aliyun.Acs.Core.Profile;
using Aliyun.Acs.Sms.Model.V20160927;
//using Aliyun.Acs.Ecs.Model.V20140526;
//using Aliyun.Acs.Sms.Model.V20160927;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWeb.Areas.Bus
{
    public class SendSms
    {
        //private static void TestDescribeInstanceAttribute()
        //{

        //    IClientProfile clientProfile = DefaultProfile.GetProfile("cn-hangzhou", "<your access key id>", "<your access key secret>");
        //    DefaultAcsClient client = new DefaultAcsClient(clientProfile);

        //    DescribeInstanceAttributeRequest request = new DescribeInstanceAttributeRequest();
        //    request.InstanceId = "<your instances id>";
        //    try
        //    {
        //        DescribeInstanceAttributeResponse response = client.GetAcsResponse(request);
        //        Console.Write(response.InstanceId);
        //    }
        //    catch (ServerException e)
        //    {
        //        Console.WriteLine(e.ErrorCode);
        //        Console.WriteLine(e.ErrorMessage);
        //    }
        //    catch (ClientException e)
        //    {
        //        Console.WriteLine(e.ErrorCode);
        //        Console.WriteLine(e.ErrorMessage);
        //    }

        //}
        public static void TestSms()
        {
            //IClientProfile profile = DefaultProfile.GetProfile("cn-hangzhou", "<your accessKey", "<your accessSecret>");
            IClientProfile profile = DefaultProfile.GetProfile("cn-hangzhou", "LTAIvbUtEOtg9Mvt", "ktNNVomKEhswB3gFsGUW9XGt99jzjc");
            IAcsClient client = new DefaultAcsClient(profile);

            SingleSendSmsRequest request = new SingleSendSmsRequest();

            try
            {

                //request.SignName = "管理控制台中配置的短信签名（状态必须是验证通过）";

                //request.TemplateCode = "管理控制台中配置的审核通过的短信模板的模板CODE（状态必须是验证通过）";

                //request.RecNum = "接收号码，多个号码可以逗号分隔";

                //request.ParamString = "短信模板中的变量；数字需要转换为字符串；个人用户每个变量长度必须小于15个字符。";
                request.SignName = "富士康南阳大巴专线";

                request.TemplateCode = "SMS_75725166";

                request.RecNum = "18611693213";

                request.ParamString = "";

                SingleSendSmsResponse httpResponse = client.GetAcsResponse(request);

            }
            catch (ServerException e)
            {

            }

            catch (ClientException e)
            {

            }
        }
    }
}
