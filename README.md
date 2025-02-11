
abuse reporting format (ARF) is the foremat used by failure reports.
3 parts human readable part, followed by a machine readable part, and the original message

1sr part:
human readable text (email body)

2nd PART - feedback-report  (note:might be in base64 handle):

Feedback-type: auth-failure

User-Agent:
Version:
Original-Mail-From:
Original-Envelope-Id:
Arrival-Date:
Authentication-Results:
Original-Envelope-Id: 
Original-Mail-From: 
Source-IP: 
Reported-Domain: 
Reported-URI:
Delivery-Result: 

new:
Auth-Failure:
Delivery-Result?:

dkim-arf:
DKIM-Domain:
DKIM-Identity:
DKIM-Selector: 
DKIM-Canonicalized-Header?:
DKIM-Canonicalized-Body?:
DKIM-Canonicalized-Body?:
DKIM-Selector-DNS

spf-arf:
SPF-DNS

3rd PART - rfc822-headers(email headers) or rfc822(full email):

rfc822-headers:
Authentication-Results:
Received:
DKIM-Signature:
Date:
Reply-To:
From:
To:
Subject:
Message-ID:
