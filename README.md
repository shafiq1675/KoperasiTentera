Overview
---
This system provides a complete user registration and verification solution with:
---
1. Mobile number verification via SMS

2. Email verification

3. Policy acceptance

4. 6-digit PIN setup

5. Seamless login functionality

--------------

Prerequisites:

1. .NET 8.0 SDK or later

2. SQL Server (2016 or later) with Windows Authentication

3. Optional: SMTP server for email sending

4. Optional: SMS gateway for mobile verification

-----------

API Endpoints
---
User Registration Flow
---------------------------------
Check Registration Status:

POST /api/auth/check-registration
  
Body: "icnNumber"

-------------------

Register New User:

POST /api/auth/register

Body: { "icnNumber", "name", "mobileNumber", "email" }

----------------

Send Mobile Verification Code:

POST /api/auth/send-mobile-verification

Body: "icnNumber"

-----------------------

Verify Mobile Code:

POST /api/auth/verify-mobile

Body: { "icnNumber", "code", "verificationType": "mobile" }

-------------------------

Send Email Verification Code:

POST /api/auth/send-email-verification

Body: "icnNumber"

------------------------------

Verify Email Code:

POST /api/auth/verify-email

Body: { "icnNumber", "code", "verificationType": "email" }

------------------------

Accept Policy:

POST /api/auth/accept-policy

Body: { "icnNumber", "accept": true }

------------------------------

Set PIN:

POST /api/auth/set-pin

Body: { "icnNumber", "pin": "123456" }
