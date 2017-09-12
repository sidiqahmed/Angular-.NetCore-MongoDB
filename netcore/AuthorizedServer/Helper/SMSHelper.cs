﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;

namespace AuthorizedServer.Helper
{
    public class SMSHelper
    {
        public static string SendSMS(string phoneNumber, string otp)
        {
            try
            {
                AmazonSimpleNotificationServiceClient smsClient = new AmazonSimpleNotificationServiceClient
                    ("AKIAJ3B7P4FXGYSUXMYA", "BcJKVujqRbxsyUlkPYSIoAoO0Z+yYXkyk6qXkIlS", Amazon.RegionEndpoint.APSoutheast1);

                var smsAttributes = new Dictionary<string, MessageAttributeValue>();

                MessageAttributeValue senderID = new MessageAttributeValue();
                senderID.DataType = "String";
                senderID.StringValue = "ArthurClive";

                MessageAttributeValue sMSType = new MessageAttributeValue();
                sMSType.DataType = "String";
                sMSType.StringValue = "Promotional";

                MessageAttributeValue maxPrice = new MessageAttributeValue();
                maxPrice.DataType = "Number";
                maxPrice.StringValue = "0.5";

                CancellationTokenSource source = new CancellationTokenSource();
                CancellationToken token = source.Token;

                smsAttributes.Add("AWS.SNS.SMS.SenderID", senderID);
                smsAttributes.Add("AWS.SNS.SMS.SMSType", sMSType);
                smsAttributes.Add("AWS.SNS.SMS.MaxPrice", maxPrice);

                string message = "Verification code for your Arthur Clive registration request is " + otp;

                PublishRequest publishRequest = new PublishRequest();
                publishRequest.Message = message;
                publishRequest.MessageAttributes = smsAttributes;
                publishRequest.PhoneNumber = phoneNumber;

                Task<PublishResponse> result = smsClient.PublishAsync(publishRequest, token);
                return "Success";
            }
            catch (Exception ex)
            {
                return "Failed";
            }
        }
    }
}