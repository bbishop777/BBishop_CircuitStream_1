
  //   var req = https.request(options, function (res) {
  //     console.log('Status: ' + res.statusCode);
  //     console.log('Headers: ' + JSON.stringify(res.headers));
  //     res.setEncoding('utf8');
  //     var chunks = [];
  //     res.on("data", function (chunk) {
  //       console.log("gathering chunks!")
  //       chunks.push(chunk);
  //     })
  //     .then(function(chunks) {
  //       var body = Buffer.concat(chunks);
  //       console.log("Am I doing the res.on?", JSON.parse(body.reult.fulfillment.speech));
  //     });

  //     // res.on("end", function () {
  //     //   // var body = Buffer.concat(chunks);
  //     //   console.log("This is the response I believe", JSON.parse(body));
  //     // });
  //   });

  //   req.on('error', function(e) {
  //       console.log('problem with request: ' + e.message);
  //   });

  //   req.write(JSON.stringify({ query: question,
  //     lang: 'en',
  //     sessionId: '1234567890' }));
  //   req.end();



  //   // var options = {
  //   //   hostname: API_HOST,
  //   //   path: API_PATH,
  //   //   lang : 'en',
  //   //   method: 'POST',
  //   //   headers: {
  //   //       'Content-Type': 'application/json; charset=utf-8',
  //   //       'Authorization' : 'Bearer '+ API_CLIENT_TOKEN,
  //   //       // 'Cache-Control': 'no-cache',
  //   //       // 'Content-Length': JSON.stringify(question).length
  //   //   },
  //   //   query : question,
  //   //   sessionId : '12345678901'
  //   // };
  //   // var req = https.request(options, function(res) {
  //   //   console.log('Status: ' + res.statusCode);
  //   //   console.log('Headers: ' + JSON.stringify(res.headers));
  //   //   res.setEncoding('utf8');
  //   //   res.on('data', function (body) {
  //   //     console.log('Body: ' + body);
  //   //     var response = JSON.parse(body);
  //   //   });
  //   // });
  //   // req.on('error', function(e) {
  //   //     console.log('problem with request: ' + e.message);
  //   // });
  //   // // // write data to request body
  //   // req.write(JSON.stringify(question));
  //   // req.end();
  // };

const App = require('actions-on-google').ApiAiApp;
const bodyParser = require('body-parser');
const express = require('express');
const req = require('request');
const http = require('http');
const https = require('https');
const readline = require('readline');
const qs = require('querystring');
// const Buffer = require('buffer/').Buffer;
const Base64 = require('js-base64').Base64;
const url = require('url');
const Vision = require("@google-cloud/vision");
const vision = Vision();
const appEx = express();
const Promise = require('bluebird');
const urlencodedParser = bodyParser.urlencoded({ extended: false })



//API.AI Query Route
const API_HOST = "api.api.ai";
const API_PATH = "/api/query?v=20150910";
const API_CLIENT_TOKEN = "48a7af9ea66b4283b37cb41b5f9015d9";
const API_PROJECT_ID = "rcuh-test-bf133";

//Microsoft Bot
const host = 'https://westus.api.cognitive.microsoft.com/qnamaker/v2.0';
const path = '/knowledgebases/34bf5bf5-4554-4da7-98bd-388b40d2cbac/generateAnswer';
const subKey = 'a561a9b4b3bd4a32aefe0a4d561aded6';

//IBM Services
const watson = require('watson-developer-cloud');
const retrieve_and_rank = watson.retrieve_and_rank({
  username: '2ad2883a-7ee5-4c5c-9cb1-dee632746914',
  password: '78XEBUwW2V3X',
  version: 'v1'
});
const params = {
    cluster_id: 'sc8687cfce_f687_4667_91a9_6d4fbab55b35',
    collection_name: 'RCUHDemoCollection',
    wt: 'json'
};
const ranker_ID = '27be5bx32-rank-11275';
solrClient = retrieve_and_rank.createSolrClient(params);

//Twilio account
const twilio = require('twilio');
const ACCOUNT_SID = 'AC9237c05f1dce2dd8b47adc4f7612dc16';
const AUTH_TOKEN = '1d447b4dc914aa697dbc3614a18b4196';
const APPLICATION_SID = 'MG9a55e94553f771bbf9d7cbe74df543e6';
const TWILIO_NUMBER = 8085186923;
const client = new twilio(ACCOUNT_SID, AUTH_TOKEN);
const MessagingResponse = twilio.twiml.MessagingResponse;
const VoiceResponse = twilio.twiml.VoiceResponse;


var projectCount = 0;

//Google Services
const google = require('googleapis');
const googleAuth = require('google-auth-library');
const plus = google.plus('v1');
const sheets = google.sheets('v4');
var customsearch = google.customsearch('v1');

//Google Email
const GMCLIENT_ID = '604093887975-rdlkt8ra57k7s04cirbbc3vjc3mq38g8.apps.googleusercontent.com';
const GMCLIENT_SECRET = 'S4ajW3pj7ecTL3-mzPHxksvB';

//Google Calendar
const GCCLIENT_ID = '604093887975-hnn5aelrmro4gtgu62otbsnoa3kv4fu4.apps.googleusercontent.com';
const GCCLIENT_SECRET = 'AMHevXSg0McMGia5f1PcEA22';

//Google Sheets:
const GSCLIENT_ID = '604093887975-ultgsk4fa2u2aqvk0s2emaoq45sg79hl.apps.googleusercontent.com';
const GSCLIENT_SECRET = 'Fm0p1EufhL6ysuDUDQnA0NVX';
var gsParams = {
      spreadsheetId: '11_oT800EjbGXyyN2VMVR6IoNbrLhr2GWZeMbLT2zPdk',
      resource: {
        valueInputOption: 'USER_ENTERED',
        data: []
      }
};

//Google API Info
const REDIRECT_URL = ["urn:ietf:wg:oauth:2.0:oob", "http://localhost:3000"];
const PROJECT_ID = 'rcuh-test-bf133';
const AUTH_URI = 'https://accounts.google.com/o/oauth2/auth';
const TOKEN_URI ='https://accounts.google.com/o/oauth2/token';
const AUTH_PROVIDER_X509_CERT_URL = 'https://www.googleapis.com/oauth2/v1/certs';
//const vision = require('@google-cloud/vision')();
const CX = '003215725753965418006:5rfk-gdtjy0';
const API_KEY = 'AIzaSyDxbOHgTrEodV3u9nrlAJfOMBOCs5zNkpA';
var rl = readline.createInterface({
  input: process.stdin,
  output: process.stdout
});
var sickNoteNeeded = false;
var sickNoteFound = false;
var gotIt;
var text = [];
var timesheetSubmitted = false;

//Google function to Authorize API calls to Google Gmail
function authorizeGM(scope, callback) {
  console.log("Im here trying to authorize Gmail");
  var auth = new googleAuth();
  var oauth2Client = new auth.OAuth2(GMCLIENT_ID, GMCLIENT_SECRET, REDIRECT_URL[0]);
  oauth2Client.credentials = { access_token: 'ya29.GlukBG5L07IYidJw8KgK3mXTgCvPfI0FHbgCFhJd5ZjmuQo7vLAhV5cZwicgyTRWjVlh_O0V0sBnQ9hec3uRoBq06VX2qMhB81PZPceJgcKwqcYFIw5sk0DKPcWV',
                               refresh_token: '1/cGPmXt-4fJs3w89z60-3jC7x2rLlHL1qhBln1GJzQKJslGwpzHtDUjD9vgJYotJU',
                               token_type: 'Bearer',
                               expiry_date: new Date().getTime() + 172800000
  };

  if(Object.keys(oauth2Client.credentials).length === 0 && oauth2Client.credentials.constructor === Object) {
    console.log('Got no token, so authentication failed...getting new token');
    return getNewToken(scope, oauth2Client, callback);
  }
  else {
    console.log("I have a token already", oauth2Client.credentials)
    return callback(oauth2Client);
  }


  // if(oauth2Client) {
  //   if(oauth2Client.hasOwnProperty('gmail') && oauth2Client.gmail === true) {
  //     console.log("I have a token already", oauth2Client.credentials)
  //     return callback(oauth2Client);
  //   }
  // }
  // else {
  //   var auth = new googleAuth();
  //   var oauth2Client = new auth.OAuth2(GMCLIENT_ID, GMCLIENT_SECRET, 'https://us-central1-rcuh-test-bf133.cloudfunctions.net/rcuhWebhook');
  //   oauth2Client.gmail = true;
  //   console.log('Got no token, so authentication failed...getting new token');
  //   return getNewToken(scope, oauth2Client, callback, response);
  // }

}
 // if(app.getUser().access_token === undefined) {
// }



//Google function to Authorize API calls to Google Sheets
function authorizeGS(scope, callback) {
  console.log("Im here trying to authorize");
  var auth = new googleAuth();
  var oauth2Client = new auth.OAuth2(GSCLIENT_ID, GSCLIENT_SECRET, REDIRECT_URL);
  oauth2Client.credentials = {  access_token: 'ya29.GluNBIsRFptWi6kT0mToG4WH43y41AiTDmUc9eglqgUy54pGgFXtmeHXEEb8ZDNJCPO1eno_qShS7n3UxGMoMfZQLNgx4aiQ-WVUx2vzbqcTjlX9UFmpl1o7NRoH',
                                refresh_token: '1/W3lpJivFituuiRuogAtItt88ftUsXp-jOYVUvAFFyYw',
                                token_type: 'Bearer',
                                expiry_date: new Date().getTime() + 172800000
                              };
  if(Object.keys(oauth2Client.credentials).length === 0 && oauth2Client.credentials.constructor === Object) {
    console.log('Got no token, so authentication failed...getting new token');
    return getNewToken(scope, oauth2Client, callback);
  }
  else {
    console.log("I have a token already", oauth2Client.credentials)
    return callback(oauth2Client);
  }
};

//Google function to Authorize API calls to Google Calendar
function authorizeGC(scope, callback) {
  var auth = new googleAuth();
  var oauth2Client = new auth.OAuth2(GCCLIENT_ID, GCCLIENT_SECRET, REDIRECT_URL);
  oauth2Client.credentials =  { access_token: 'ya29.GluOBBL3YW93IBziB28Ch3oboakulZyQPS_Ap_TTvlQz6RQK0RKHqkg2f-EOVm_MxoL8XyJLAUAiXkxUYn5CKNcoeU3RRmJQxJVW8_THRINiKqYi6ALFiI9WHZNz',
                                 refresh_token: '1/zT-SzSHbTMU_jqgEWhIYKsuQaJ5H1WOkqSZFplrsmYtAZ9FWpWMVjUNyV9NQoRtu',
                                 token_type: 'Bearer',
                                 expiry_date: new Date().getTime() + 172800000
                               };

  if(Object.keys(oauth2Client.credentials).length === 0 && oauth2Client.credentials.constructor === Object) {
    console.log('Got no token, so authentication failed...getting new token');
    return getNewToken(scope, oauth2Client, callback);
  }
  else {
    console.log("I have a token already", oauth2Client.credentials)
    return callback(oauth2Client);
  }
};


//Google Token: To create the intitial token...then hardcode it as oauth2Client.credentials in Authorize function
function getNewToken(scope, oauth2Client, callback) {
  if(oauth2Client.gmail === true) {
    var authUrl = oauth2Client.generateAuthUrl({
      access_type: 'offline',
      scope: scope,
      include_granted_scopes: true,
      state: 'state_parameter_passthrough_value',
      response_type: 'code',
      prompt: 'select_account'
    });
    console.log("let me see authUrl", authUrl);

    response.writeHead(302, {Location: authUrl});
    response.end();
  }
  else {
    var authUrl = oauth2Client.generateAuthUrl({
      access_type: 'offline',
      scope: scope
    });
    console.log('Authorize this app by visiting this url: ', authUrl);
    rl.question('Enter the code from that page here: ', function(code) {
      rl.close();
      oauth2Client.getToken(code, function(err, tokens) {
        if (err) {
          console.log('Error while trying to retrieve access token', err);
          return callback(err);
        }
        else {
          oauth2Client.setCredentials(tokens);
          console.log("lets look at the token", tokens, "and lets see the creds", oauth2Client)
          return callback(oauth2Client);
        }
      });
    });
  }
};

function updateTimesheet(oauth2Client) {
  var request = {
    spreadsheetId: '11_oT800EjbGXyyN2VMVR6IoNbrLhr2GWZeMbLT2zPdk',
    range: 'D16:G20',
    auth: oauth2Client
  };
  gsParams.auth = oauth2Client;
  console.log("What are our gsParams", gsParams);
  sheets.spreadsheets.values.clear(request, function(err, response) {
    if (err) {
      console.log('The API returned an error when clearing: ' + err);
      return;
    }
    else {
      sheets.spreadsheets.values.batchUpdate(gsParams, function(err, response) {
        if (err) {
          console.log('The API returned an error when batchUpdate: ' + err);
          return;
        }
        else {
          timesheetSubmitted = true;
          var scopeGM =['https://mail.google.com/'];
          authorizeGM(scopeGM, sendEmail);
        }
      })
    }
  });
};

function sendEmail(oauth2Client) {
  console.log("here sending email!")
  var to = "Bradley Bishop <bradley_bishop@datahouse.com>";
  var cc = 'dhtest724@gmail.com';
  var from = "RCUH Bot <dhtest724@gmail.com>";
  var subject = "Submitted Timesheet";
  var message = "Here is your timesheet: https://docs.google.com/spreadsheets/d/11_oT800EjbGXyyN2VMVR6IoNbrLhr2GWZeMbLT2zPdk/edit?usp=sharing";
  var constructEmail = ["Content-Type: text/plain; charset=\"UTF-8\"\n",
    "MIME-Version: 1.0\n",
    "Content-Transfer-Encoding: 7bit\n",
    "to: ", to, "\n",
    "cc: ", cc, "\n",
    "from: ", from, "\n",
    "subject: ", subject, "\n\n",
    message
    ]
    .join('');
  var encodeEmail = new Buffer.from(constructEmail).toString("base64").replace(/\+/g, '-').replace(/\//g, '_');
  var gmail = google.gmail('v1');
  var request = {
    auth: oauth2Client,
    userId: 'dhtest724@gmail.com',
    resource: {
      raw: encodeEmail
    }
  };
  gmail.users.messages.send(request, function(err, response) {
    if (err) {
      console.log('The Gmail API returned an error: ' + err);
      return;
    }
    else {
      console.log('Here is my response',response);
    }

  });
};


//FUNCTIONALITY OPEN TO API.AI CALLS:
exports.rcuh = (request, response) => {
  console.log("Heres the request method: ", request.method);
  

  if(request.method === 'GET') {
    console.log("Just got a get request");
    var objTwil = qs.parse(request.url);
    console.log("Here is json", objTwil);
    if(!(objTwil.MediaContentType0 === undefined) && objTwil.MediaContentType0 === "image/jpeg") {
      console.log("Ok..we have an image present!")
      var textedImage = objTwil.MediaUrl0;
      vision.webDetection({source: { imageUri: textedImage }}, function(err, retResponse) {
        if (err) {
          console.log(err);
        } 
        else {
          if (retResponse.webDetection.webEntities[0].description === "Physician") {
            sickNoteFound = true;
            var twiml = new MessagingResponse();
            twiml.message("I confirmed your medical excuse. I will log that you worked 8 hours everyday except for " + text + "."
            + " You should be getting a confirmation email "
            + "within the next few minutes.  Have a great weekend!");
            response.writeHead(200, {'Content-Type': 'text/xml'});
            response.end(twiml.toString());
            console.log("Sending approval to twilio",gsParams);
            var scopeGS = ['https://www.googleapis.com/auth/spreadsheets'];
            authorizeGS(scopeGS, updateTimesheet);
          }
          else if (retResponse.webDetection.webEntities[0].description !== "Physician") {
            sickNoteFound = false;
            var twiml = new MessagingResponse();
            twiml.message("Your image did not meet the criteria for a proper medical excuse.  Please speak to HR. Thank you.");
            response.writeHead(200, {'Content-Type': 'text/xml'});
            response.end(twiml.toString());
            console.log(retResponse);
          }
        }
      });
    }
  } 
  else {
    console.log("what is the request.body", request.body)
    if(request.body.Digits || request.body.SpeechResult) {
      console.log("are you making it here???", request.body.Digits)
      if(request.body.Digits === '1' || request.body.SpeechResult === 'one') {
        console.log("You want sick leave, right?");
        const responseTwil = new VoiceResponse();
        responseTwil.say(
          {
            voice: 'alice',
          },
          "You have used up all 5 unexcused sick days.  You will have to submit a doctors excuse to receive any further ssick pay. Thank you.  Goodbye!"
        )
        response.type('text/xml');
        response.send(responseTwil.toString());
      }
      else if(request.body.Digits === '2' || request.body.SpeechResult === 'two') {
        console.log("You want vacation leave, right?");
        const responseTwil = new VoiceResponse();
        responseTwil.say(
          {
            voice: 'alice',
          },
          "You currently have 88 hours of vacation leave available. Thank you and Goodbye!"
        )
        response.type('text/xml');
        response.send(responseTwil.toString());
      }
      else if(request.body.Digits === '3' || request.body.SpeechResult === 'three') {
        console.log("You want to end the call, right?");
        const responseTwil = new VoiceResponse();
        responseTwil.say(
          {
            voice: 'alice',
          },
          "Thank you and Goodbye!"
        )
        response.type('text/xml');
        response.send(responseTwil.toString());
      }
    }
    const app = new App({ request, response});
    var hours;
    var inputType = app.getInputType()

    function sendPassphrase(app) {
      client.calls.create({
        url: 'https://storage.googleapis.com/rcuh-test-bf133.appspot.com/Issue.xml',        
        to: '+18082220118',
        from: '+18085186923',
        method: 'GET'
      }, function(err, call) {
          if(err) {
            console.log("i got an error",err);
          }
          else {
            console.log("call successful!", call.sid)
          }
      })
      app.ask("Calling");
    };

    function reset(app) {
      sickNoteNeeded = false;
      sickNoteFound = false;
      gotIt = null;
      text = [];
      timesheetSubmitted = false;
      console.log("What is timesheetSubmitted?", timesheetSubmitted);
      app.ask("Timesheet process is reset!")
    }
    
    function identity(app){
      var text_to_speech = '<speak>'
      + '<audio src="https://storage.googleapis.com/rcuh-test-bf133.appspot.com/Track3.mp3"></audio>Just kidding! Im BeakerBot.'
      + '</speak>'
      app.ask(text_to_speech);
    }
    
    function sound(app) {
      var text_to_speech = '<speak>'
      + 'Oh yes!  Johnny-Five from Short Circuit!  He says stuff like:'
      + '<audio src="https://www.johnny-five.com/simplenet/Shortcircuit/Sounds/Allnew.wav"></audio>Man, I want my own utility pack.'
      + '</speak>'
      app.ask(text_to_speech);
    }
    
     /*
    This function is the function that will be called when the user initiates the conversation.  When the user
    is on their google assistant app, it will show suggestion chips
    */
    function welcome(app) {
      console.log("On welcome function...timesheetSubmitted is ", timesheetSubmitted);
      // if(request.body.originalRequest.source === 'twilio') {
      //   console.log("this is the body", request.body)
      // };
      var scopeGC = ['https://www.googleapis.com/auth/calendar'];
      authorizeGC(scopeGC, checkTimecardDeadline);
      // console.log("this is request", request.body.originalRequest)
    };

    function checkTimecardDeadline(oauth2Client) {
      var event = "Submit timecard";
      var currentDate = new Date();
      currentDate = currentDate.toISOString();
      var adjustDate = currentDate.split(/\D/);
      adjustDate = new Date(adjustDate[0], adjustDate[1]-1, adjustDate[2], adjustDate[3], adjustDate[4], adjustDate[5]).toISOString().split("T");
      adjustDate = adjustDate[0];
      console.log("What is adjustDate", currentDate, adjustDate);
      var responseToSend;
      var calendar = google.calendar('v3');
      console.log("did this run?", adjustDate);
      var request = {
          auth: oauth2Client,
          calendarId: 'primary',
          q: event,
          timeMin: currentDate,
      };
      calendar.events.list(request, function(err, response) {
        if (err) {
          console.log("The API returned an error: " + err + "\nThe request was : " + request);
        } 
        else if (response.items.length > 0) {
          responseToSend = response.items[0].start.date;
          console.log("Has timesheet been submitted?", timesheetSubmitted, responseToSend, " Compared to: ", adjustDate );
          if((responseToSend === adjustDate) && (timesheetSubmitted === false)) {
            console.log("It's a match");
            var callTwilio = client.messages.create({
              to: '+18082220118',
              from: '+18085186923',
              body: 'Today is the deadline to submit your timecard.',
            });
            callTwilio.then(function(message) {
              console.log("Text success!", message)
            }, function(error) {
                console.error('Text failed!  Reason: '+error.message);
            });
             
            if (inputType === "VOICE") {
              var text_to_speech = '<speak>'
                +'Hi there!  Im BeakerBot'
                + '<audio src="https://storage.googleapis.com/rcuh-test-bf133.appspot.com/BeakerIntro3.mp3"></audio>Im here to assist you.  However, I see today is the deadline for timesheet submission.  To enter it, just say Timesheet.'
                + '</speak>'
              app.ask(text_to_speech);            
            }
            else if ((inputType === "TOUCH") || (inputType === "KEYBOARD")) {
              app.ask("Hello! I'm BeakerBot and here to assist you. However, I see today is the deadline for timesheet submission.  You can type 'Timesheet' to enter it or ask something else!")
              app.ask(app.buildRichResponse()
                .addSuggestions(['Enter Timesheet', 'Hear Announcements', 'Contact RCUH', 'Look at Calendar', 'Q & A'])
              )
            }
          }    
          else {
            console.log("Got no match");
            if (inputType === "VOICE") {
              var text_to_speech = '<speak>'
                +'Hi there!  Im BeakerBot'
                + '<audio src="https://storage.googleapis.com/rcuh-test-bf133.appspot.com/BeakerIntro3.mp3"></audio>I can assist you.'
                + '</speak>'
              app.ask(text_to_speech);
            }
            else if ((inputType === "TOUCH") || (inputType === "KEYBOARD")) {
              app.ask("Hi there! I'm BeakerBot. How can I assist you?");
              app.ask(app.buildRichResponse()
                .addSuggestions(['Enter Timesheet', 'Hear Announcements', 'Contact RCUH', 'Look at Calendar', 'Q & A'])
              );
            }
          }
        }             
        else {
          console.log("No items from calendar returned");
          if (inputType === "VOICE") {
            var text_to_speech = '<speak>'
              +'Hi there!  Im BeakerBot'
              + '<audio src="https://storage.googleapis.com/rcuh-test-bf133.appspot.com/BeakerIntro3.mp3"></audio>I can assist you.'
              + '</speak>'
            app.ask(text_to_speech);
          }
          else if ((inputType === "TOUCH") || (inputType === "KEYBOARD")) {
            app.ask("Hi there! I'm BeakerBot. How can I assist you?");
            app.ask(app.buildRichResponse()
              .addSuggestions(['Enter Timesheet', 'Hear Announcements', 'Contact RCUH', 'Look at Calendar', 'Q & A'])
            );
          } 
        }
      })
    };
      
    
    function help (app) {
      app.askWithCarousel('Here is what I can do',
        app.buildCarousel()
         .addItems([
           app.buildOptionItem("Hear Announcements")
              .setTitle("Hear Announcements")
              .setDescription("Keep in the know of everything at RCUH by asking to hear announcements!")
              .setImage("https://cdn3.iconfinder.com/data/icons/communication-icons-3/512/Megaphone-512.png", "Bullhorn icon"),
           app.buildOptionItem("Q & A")
             .setTitle("Q & A")
             .setDescription("Ask me any question about RCUH and I will try my best to answer it!")
             .setImage("https://lh5.googleusercontent.com/VSOdT47hUKt0vyoVSkCh9-T7CFevmIcoCkskHp8zEmA8jTsLdsHihk-AcLMDOt30CE9H_n_l2LzHtNxQLyWtt6VeTdEfjsoaGgpiuxjGxuIxgsGCny2m3UrxkQs96GwcIRPyv4c", "Q & A icon"),
           app.buildOptionItem("Enter Timesheet")
             .setTitle('Enter in Timesheet')
             .setDescription("That's right, I can help put in your timesheet!  Just give me your passphrase I assigned to you this week and you will be able to enter your hours.")
             .setImage("https://png.icons8.com/google-sheets/color/1600", "Sheets icon"),
           app.buildOptionItem("Contact RCUH")
              .setTitle("Contact RCUH")
              .setDescription("Looking to contact a certain person or department?  Ask me and I will try to provide with the info!")
              .setImage("https://maxcdn.icons8.com/Share/icon/Very_Basic//contacts1600.png", "Contact icon"),
           app.buildOptionItem("Calendar")
              .setTitle("Look at Calendar")
              .setDescription("Keep track of your deadlines by asking me when a certain event occurs!")
              .setImage("http://icons.iconarchive.com/icons/dtafalonso/android-lollipop/512/calendar-icon.png", "Calendar icon")
         ]));
    };
    
    
    function quote(app) {
      console.log("I'm here on quote!");
        var cost = app.getArgument('cost');
        var purchaseResponse;
        if (cost < 3500) {
          purchaseResponse = 'Purchases $3,500 or below are made with the most advantageous price, delivery terms, and other factors affecting the total cost of the required item(s). If the DUO considers and certifies the price(s) to be reasonable then no additional quotations are needed.';
        }
        else if (cost >= 3500 && cost < 25000) {
          purchaseResponse = 'Purchases greater than $3,500 but less than $25,000 require three or more verbal quotations.';
        }
        else {
          purchaseResponse = 'Purchases greater than $25,000 require three or more written quotations.';
        }
        app.ask(purchaseResponse);
    };
    
    // As the default fallback intent, this method will handle all the queries that were not caughts by the other intents.  This
    // method will first doing an intelligent search using our IBM Retrieve and Rank service and if nothing is found, will do a keyword
    // search using our Google custom engine.
    function searchRR(app) {
      if(!(app.getContextArgument('actions_intent_option', 'OPTION') === null)) {
        const param = app.getContextArgument('actions_intent_option','OPTION').value;
        console.log("did you make it past the null thing?", param);
        switch (param){
          case "Hear Announcements":
            response.setHeader('Content-Type', 'application/json'); //Requires application/json MIME type
            response.send(JSON.stringify(
                { "speech": "",
                  "displayText": "",
                  "data": {},
                  "contextOut": [
                    {"name":"general-announcements",
                     "lifespan":2,
                     "parameters":{}
                    }
                  ],
                  "source": "",
                  "followupEvent":{"name":"makeAnnouncements","data":{}}
                }
              )
            );
            break;
          case "Q & A":
            response.setHeader('Content-Type', 'application/json'); //Requires application/json MIME type
            response.send(JSON.stringify(
                { "speech": "",
                  "displayText": "",
                  "data": {},
                  "contextOut": [
                    {"name":"I_have_a_question",
                     "lifespan":2,
                     "parameters":{}
                    }
                  ],
                  "source": "",
                  "followupEvent":{"name":"questionUndefined","data":{}}
                }
              )
            );
            break;
          case "Enter Timesheet":
            response.setHeader('Content-Type', 'application/json'); //Requires application/json MIME type
            response.send(JSON.stringify(
                { "speech": "",
                  "displayText": "",
                  "data": {},
                  "contextOut": [
                    {"name":"hr-timesheet",
                     "lifespan":2,
                     "parameters":{}
                    }
                  ],
                  "source": "",
                  "followupEvent":{"name":"timesheet_start","data":{}}
                }
              )
            );
            break;
          case "Contact RCUH":
            response.setHeader('Content-Type', 'application/json'); //Requires application/json MIME type
            response.send(JSON.stringify(
                { "speech": "",
                  "displayText": "",
                  "data": {},
                  "contextOut": [
                    {"name":"general-contacts-hr",
                     "lifespan":2,
                     "parameters":{}
                    }
                  ],
                  "source": "",
                  "followupEvent":{"name":"getContacts","data":{}}
                }
              )
            );
            break;
          case "Calendar":
            console.log("Did I get to calendar?");
            response.setHeader('Content-Type', 'application/json'); //Requires application/json MIME type
            response.send(JSON.stringify(
                { "speech": "",
                  "displayText": "",
                  "data": {},
                  "contextOut": [
                    {"name":"payroll-calendar",
                     "lifespan":2,
                     "parameters":{}
                    }
                  ],
                  "source": "",
                  "followupEvent":{"name":"checkCalendar","data":{}}
                }
              )
            );
            break;
          default:
            response.setHeader('Content-Type', 'application/json'); //Requires application/json MIME type
            response.send(JSON.stringify(
                { "speech": "",
                  "displayText": "",
                  "data": {},
                  "contextOut": [
                    {"name":"",
                     "lifespan":2,
                     "parameters":{}
                    }
                  ],
                  "source": "",
                  "followupEvent":{"name":"inputUnknown","data":{}}
                }
              )
            );
            break;
        };
    
      }
      else {
        var fallbackQuestion = request.body.result.resolvedQuery;
        var queryRanker = qs.stringify({
            solr_clustr_id: 'sc8687cfce_f687_4667_91a9_6d4fbab55b35',
            collection_name: 'RCUHDemoCollection',
            start: 0,
            rows: 21,
            q: fallbackQuestion,
            ranker_id: ranker_ID,
            fl: '*,ranker.confidence'
        });
        solrClient.get('fcselect', queryRanker, function(err, searchResponse) {
           console.log(searchResponse.response.numFound);
            if (err) {
                console.log('Error searching for documents: ' + err);
                app.ask("I'm sorry but I received an error searching")
            }
            else if (searchResponse.response.numFound > 0 && searchResponse.response.docs[0]["ranker.confidence"] >= .5) {
              buildResponse(searchResponse.response.docs[0]);
              response.end(searchResponse.response.docs[0].searchText[1]);
            }
            else {
              customsearch.cse.list({
                  cx: CX,
                  q: fallbackQuestion,
                  auth: API_KEY
              }, function(err, resp) {
                  if (err) {
                      app.ask('An error occured', err);
                  }
                  else {
                    if (!resp.items || resp.items.length == 0) {
                      var inputType = app.getInputType();
                      console.log("Im getting an inputType of: ", inputType)
                      if (inputType === "VOICE") {
                        var text_to_speech = '<speak>'
                          +'Sorry, I used my big AI brain to search your policies and then I asked my friend Google but the response was fishy and I said:'
                          + '<audio src="https://storage.googleapis.com/rcuh-test-bf133.appspot.com/Track6.mp3"></audio>So would you like me to email your question to an actual human being, wait I mean an actual Human Resources Rep.'
                          + '</speak>'
                        app.ask(text_to_speech);                      
                      }
                      else if ((inputType === "TOUCH") || (inputType === "KEYBOARD")) {
                        var prompt = "Sorry but I couldn't find anything after searching Google.  Did you want me to send an email to an HR Representative?";
                        response.setHeader('Content-Type', 'application/json'); //Requires application/json MIME type
                        response.send(JSON.stringify(
                            { "speech": prompt,
                              "displayText": prompt,
                              "data": {},
                              "contextOut": [
                                {"name":"DefaultFallbackIntent-followup",
                                 "lifespan":1,
                                 "parameters":{"question": fallbackQuestion}
                                }
                              ],
                              "source": ""
                            }
                          )
                        );                        
                      }
                    }
                    else {
                      buildGoogleSearchCard(resp.items[0]);
                    }
                  }
              });
            }
        });
      }
    };
    
    function sendTicket() {
      console.log("I am now in the send Ticket function")
      var scopeGM =['https://mail.google.com/'];
      authorizeGM(scopeGM, storeQuestion);
      function storeQuestion(oauth2Client) {
        var targetContext = (request.body.result.contexts.length) - 1;
        fallbackQuestion = request.body.result.contexts[targetContext].parameters.question;
        buildTicket(oauth2Client, fallbackQuestion);
      }
    }
    
    function buildTicket(oauth2Client, fallbackQuestion) {
      var to = "RCUH Bot <dhtest724@gmail.com>"
      var cc = "bradley_bishop@datahouse.com";
      var from = "RCUH Bot <dhtest724@gmail.com>";
      var subject = "Policy Q & A";
      var message = "The user at 'bradley_bishop@datahouse.com' has asked the question '" + fallbackQuestion + "' but I was not able to find a response.  This email has been sent to both HR and to the user.  HR will handle this concern.";
      var constructEmail = ["Content-Type: text/plain; charset=\"UTF-8\"\n",
      "MIME-Version: 1.0\n",
      "Content-Transfer-Encoding: 7bit\n",
      "to: ", to, "\n",
      "cc: ", cc, "\n",
      "from: ", from, "\n",
      "subject: ", subject, "\n\n",
      message
      ]
      .join('');
      var encodeEmail = new Buffer.from(constructEmail).toString("base64").replace(/\+/g, '-').replace(/\//g, '_');
      var gmail = google.gmail('v1');
      var request = {
        auth: oauth2Client,
        userId: 'dhtest724@gmail.com',
        resource: {
          raw: encodeEmail
        }
      };
      gmail.users.messages.send(request, function(err, response) {
        if (err) {
          console.log('The Gmail API returned an error: ' + err);
          return;
        }
        else {
          app.ask("We have submitted a ticket for you and an HR Representative will be in contact with you shortly.")
          console.log('Here is my response',response);
        }
    
      });
    }
    
    // Takes in a text response from the google search and will create a card rich response to be displayed on
    // a phone screen.  The card will link to the website.
    function buildGoogleSearchCard(data) {
       var speechResult = "The top result was " + data.title + ".";
       if (app.hasSurfaceCapability(app.SurfaceCapabilities.SCREEN_OUTPUT)) {
         app.ask(app.buildRichResponse()
          .addSimpleResponse("Here's the top result from Google")
          .addBasicCard(
            app.buildBasicCard(data.snippet)
              .setTitle(data.title)
              .addButton("Read more", data.link)
         ));
       } else {
         app.ask(speechResult);
       }
    };
    
    // Takes in the data from the IBM intelligent search and creates a card rich response with that.  The card will contain a small snippet of the
    // data and then a link to the specific website for that policy
    function buildResponse(data) {
      var keyword = data.fileName;
      var text = data.searchText[1];
      if (app.hasSurfaceCapability(app.SurfaceCapabilities.SCREEN_OUTPUT)) {
          if (keyword.match('service')) {
              app.ask(app.buildRichResponse()
                  // Create a basic card and add it to the rich response
                  .addSimpleResponse("Here's what I found")
                  .addBasicCard(app.buildBasicCard(text)
                      .addButton('Read more', 'https://www.rcuh.com/3-000/3-400/3-440/')
                  ));
          } else if (keyword.match('moving')) {
              app.ask(app.buildRichResponse()
                  // Create a basic card and add it to the rich response
                  .addSimpleResponse("Here's what I found")
                  .addBasicCard(app.buildBasicCard(text)
                      .addButton('Read more', 'https://www.rcuh.com/3-000/3-200/3-215/')
                  ));
          } else {
              app.ask(app.buildRichResponse()
                  // Create a basic card and add it to the rich response
                  .addSimpleResponse("Here's what I found")
                  .addBasicCard(app.buildBasicCard(text)
                      .addButton('Read more', 'https://www.rcuh.com/3-000/3-200/3-220/')
                  ));
          }
      } else {
          app.ask(text);
      }
    };
    
    function generalProjectImpact(app) {
      projectCount++;
      var simpleResponse;
      var cardDescription;
      var cardTitle;
      var imageUrl;
      var imageHover;
    
      if (projectCount % 3 == 1) {
        simpleResponse = `RCUH provides the financial, human resources, and other support services to projects that expand the boundaries of knowledge and/or utilize creative ways to apply research findings in real-world situations.
        \n\n Here is one example of a RCUH project - O‘ahu Army Natural Resources Project: Philip Taylor, Principal Investigator.`;
        cardDescription = `This project protects a variety of
          creatures, including the O‘ahu elepaio, an inquisitive flycatcher.
          Yearly monitoring of breeding activity and ongoing active protection
          from invasive rodents have resulted in a population increase of the
          O‘ahu elepaio, which is a federally endangered forest bird.`;
        cardImageUrl = 'https://storage.googleapis.com/rcuh-test-bf133.appspot.com/natural_resources.png';
      }
      else if (projectCount % 3 == 2) {
        simpleResponse = `RCUH provides the financial, human resources, and other support services to projects that expand the boundaries of knowledge and/or utilize creative ways to apply research findings in real-world situations.
        \n\n Here is one example of a RCUH project - JIMAR Sustaining Healthy Coastal Ecosystems: Mark Merrifield, Principal Investigator.`;
        cardDescription = `This project leads an integrated multi-partner
        and interdisciplinary program of ecosystem assessment and
        long-term monitoring, benthic habitat mapping, and applied research on the
        coral reef ecosystems of 40 primary islands and atolls in the Hawaiian
        Archipelago, the Mariana Archipelago, American Samoa, and the Pacific
        Remote Island Areas. It also develops capacity and provides scientific
        expertise and technical partnerships to governments and key partners in
        these regions to inform and support the effective management of coral
        reef ecosystems and sustainable fisheries. The photo shows the 14,606kg
        of marine debris removed from the shorelines of the Midway Atoll National
        Wildlife Refuge.`;
        cardImageUrl = 'https://storage.googleapis.com/rcuh-test-bf133.appspot.com/JIMAR_Sustaining_Healthy_Coastal_Ecosystems.png';
      }
      else {
        simpleResponse = `RCUH provides the financial, human resources, and other support services to projects that expand the boundaries of knowledge and/or utilize creative ways to apply research findings in real-world situations.
        \n\nHere is one example of an RCUH project - Laysan Albatross Egg Swap at Pacific Missile Range Facility: David Duffy, Principal Investigator.`;
        cardDescription = `The Laysan albatross returns each year to nest
        at Kaua‘i’s Pacific Missile Range Facility, creating a significant
        bird airstrike hazard for the birds as well as for pilots. To decrease
        the albatross population at this site, eggs are taken from active nests
        and swapped with inviable eggs. The fertile eggs are provided to the
        Laysan albatross rearing project on O‘ahu, where biologists are
        attempting to establish a nesting population of the birds. `;
        cardImageUrl = 'https://storage.googleapis.com/rcuh-test-bf133.appspot.com/Laysan_Albatross_Egg_Swap_at_Pacific_Missile_Range_Facility.png';
      }
      cardTitle = simpleResponse;
      cardImageHover = simpleResponse;
    
      if (app.hasSurfaceCapability(app.SurfaceCapabilities.SCREEN_OUTPUT)) {
        app.ask(app.buildRichResponse()
        // Create a basic card and add it to the rich response
          .addSimpleResponse(simpleResponse)
          .addBasicCard(app.buildBasicCard(cardDescription)
            .setTitle(cardTitle)
            .addButton('Read more', 'https://www.rcuh.com/wp-content/uploads/2016/06/2016-RCUH-Annual-Report-FINAL.pdf')
            .setImage(cardImageUrl, cardImageHover)
          )
        );
      }
      else {
        app.ask(simpleResponse + "\n\n" + cardDescription);
      }
    
    };
    
    //Provides the contact information (phone, email, and mailing) for the various departments of RCUH.
    function contacts(app) {
      var department = app.getArgument('hr_contact_info');
      var comm = app.getArgument('info_type');
      if (department == "general") {
        app.ask("General inquires can be emailed to rcuh@rcuh.com.");
        }
      else if (department == "HR & Payroll") {
        if (comm == "phone") {
          app.ask("The phone number for the " + department + ' department is (808) 956-3100.');
        }
        else if (comm == "email") {
          app.ask("The email address for the " + department + " department is rcuhhr@rcuh.com.");
        }
        else if (comm == "mailing") {
          app.ask("The mailing address for the " + department + ` department is:\n1601 East-West Road Burns Hall 4th Floor, Makai Wing\nHonolulu, HI 96848.`);
        }
      }
      else if (department == "Disbursing/Procurement") {
        if (comm == "phone") {
          app.ask("The phone number for the " + department + ' department is (808) 956-3608.');
        }
        else if (comm == "email") {
          app.ask("I'm sorry, but the " + department + " department doesn't have an email address.");
        }
        else if (comm == "mailing") {
          app.ask("The mailing address for the " + department + " department is:\n1601 East-West Road Burns Hall, Room 4020\nHonolulu, HI 96848.");
        }
      }
      else if (department == "Accounting & Project Admin") {
        if (comm == "phone") {
          app.ask("The phone number for the " + department + " department is (808) 988-8300.");
        }
        else if (comm == "email") {
          app.ask("I'm sorry, but the " + department + " department doesn't have an email address.");
        }
        else if (comm == "mailing") {
          app.ask("The mailing address for the " + department + " department is:\n2800 Woodlawn Drive, Suite 200\nHonolulu, HI 96822.")
        }
      }
      else if (department == "Executive Director") {
        if (comm == "phone") {
          app.ask("The phone number for the " + department + " department is (808) 988-8311");
        }
        else if (comm == "email") {
          app.ask("I'm sorry, but the " + department + " department doesn't have an email address.");
        }
        else if (comm == "mailing") {
          app.ask("The mailing address for the " + department + " department is:\n2800 Woodlawn Drive, Suite 200\nHonolulu, HI 96822.");
        }
      }
      else if (department == "Corporate Services") {
        if (comm == "phone") {
          app.ask("The phone number for the " + department + " department is (808) 988-8314.");
        }
        else if (comm == "email") {
          app.ask("I'm sorry, but the " + department + " department doesn't have an email address.");
        }
        else if (comm == "mailing") {
          app.ask("The mailing address for the " + department + " department is:\n2800 Woodlawn Drive, Suite 200\nHonolulu, HI 96822.");
        }
      }
    };
    
    //Provides RCUH announcemens based on the entity provided (HR and general).
    function announcements(app) {
      var type = app.getArgument('announcement_type')
      if (type == 'human resources'){
        app.ask("The RCUH Human Resources Portal and Employee Self-Service will be down Monday, 7/10/17 from 6:00 PM to 6:30 PM for system maintenance.");
      }
      else if (type == 'general'){
        app.ask("Financial System Processing Now Available: The RCUH fiscal year-end processing has been completed. Fiscal  Administrators may now approve on-line payments and purchase orders in the Financial Portal. The first check run for fiscal year 2018 will be on Thursday, July 6, 2017 at 4:00 PM.");
      }
    };
    
    
    
    // Takes in what event the user is looking for, a time frame, and the number of occurences the user wants to hear.
    // If there is no timeframe given, we will check if there is a specified number of occurences provided and if there
    // is we will make an api call as that being the number of maxResults.  If it is not given, we assume that the user
    // only wants the next occurence and so the maxResults is set to 1.
    function calendar (app) {
      var event = app.getArgument('events');
      var timePeriod = app.getArgument('date-period');
      var numResults = app.getArgument('occurence');
      var currentDate = new Date();
      currentDate = currentDate.toISOString();
      var time = "T00:00:00Z";
      var startDate = ""
      var endDate = null;
      if (timePeriod) {
        timeFrame = timePeriod.split("/");
        startDate = timeFrame[0] + time;
        endDate = timeFrame[1] + time;
      } else {
        startDate = currentDate;
      }
      var scopeGC = ['https://www.googleapis.com/auth/calendar'];
      authorizeGC(scopeGC, listEvents);
      function listEvents(oauth2Client) {
          var calendar = google.calendar('v3');
          var request = {
              auth: oauth2Client,
              calendarId: 'primary',
              q: event,
              timeMin: startDate,
          };
          if (endDate !== null) {
            request.timeMax = endDate;
          } else {
            if (numResults === null) {
              request.maxResults = 1;
            } else {
              request.maxResults = numResults;
            }
          }
          calendar.events.list(request, function(err, response) {
              if (err) {
                  app.ask("The API returned an error: " + err + "\nThe request was : " + request);
              } else {
                if (response.items.length == 0) {
                  app.ask("I can't find any record of that event");
                } else {
                  var responseToSend = "On " + response.items[0].start.date;
                  for (var i = 1; i < response.items.length; i++) {
                    console.log(response.items[i].start.date);
                    if (i == (response.items.length - 1)) {
                      responseToSend += " and " + response.items[i].start.date;
                    } else {
                      responseToSend += ", " + response.items[i].start.date;
                    }
                  }
                  app.ask(responseToSend);
                }
              }
          });
      }
    };
    
    function microsoftBot(app) {
      var contextVar = app.getContext("mqna");
      console.log(contextVar);
      var quest = request.body.result.resolvedQuery;
      var question = {
          'question' : quest
      };
      console.log("what is question?", question);
      var options = {
        hostname: 'westus.api.cognitive.microsoft.com',
        path: '/qnamaker/v2.0/knowledgebases/b2c575f6-9fe7-4d41-8af9-27ed4b22dddd/generateAnswer',
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'Ocp-Apim-Subscription-Key' : subKey,
            'Cache-Control': 'no-cache',
            'Content-Length': JSON.stringify(question).length
        }
      };
      var reqB = https.request(options, function(res) {
        console.log('Status: ' + res.statusCode);
        console.log('Headers: ' + JSON.stringify(res.headers));
        res.setEncoding('utf8');
        res.on('data', function (body) {
          console.log('Body: ' + body);
          var response = JSON.parse(body).answers[0].answer;
          var final = response.replace(/&#39;/g, "'").replace(/&quot;/g, '"');
          app.ask(final);
        });
      });
      reqB.on('error', function(e) {
          console.log('problem with request: ' + e.message);
          app.ask("Not found. Try again.");
      });
      // write data to request body
      reqB.write(JSON.stringify(question));
      reqB.end();
    };
    
    
    
    /******** THIS IS WHERE ALL THE TIMESHEET FUNCTIONS ARE ****************/
    function getHours(app) {
      hours = parseInt(app.getArgument('totalHours'));
      console.log("What's me hours?", hours)
      if (hours < 40) {
        app.ask("Contact HR to approve unpaid leave.  Shouldn't have skipped work!")
      }
      else if (hours == 40) {
        console.log("I should reroute to exactly40");
        response.setHeader('Content-Type', 'application/json'); //Requires application/json MIME type
        response.send(JSON.stringify(
            { "speech": "",
              "displayText": "",
              "data": {},
              "contextOut": [
                {"name":"hr-timesheet-nonexempt-getHours-followup",
                 "lifespan":2,
                 "parameters":{}
                }
              ],
              "source": "",
              "followupEvent":{"name":"exactly40","data":{}}
            }
          )
        );
      }
      else if (hours > 40) {
        console.log("this is over 40");
        var contextVar = app.getContext("hr-timesheet-exempt-followup");
        if (contextVar !== null) {
          app.ask("You aren't allowed any overtime as an exempt employee.  I will log 40 full hours for you.");
          console.log("This is the context var" + contextVar);
          response.setHeader('Content-Type', 'application/json'); //Requires application/json MIME type
          response.send(JSON.stringify(
              { "speech": "",
                "displayText": "",
                "data": {},
                "contextOut": [
                  {"name":"testIt",
                   "lifespan":2,
                   "parameters":{}
                  }
                ],
                "source": "",
                "followupEvent":{"name":"exactly40","data":{}}
              }
            )
          )
        }
    
        else {
          response.setHeader('Content-Type', 'application/json');
          response.send(JSON.stringify(
            { "speech": "",
              "displayText": "",
              "data": {},
              "contextOut": [
                {"name":"testIt",
                 "lifespan":2,
                 "parameters":{}
                }
              ],
              "source": "",
              "followupEvent":{"name":"over40","data":{}}
            })
          );
        }
      }
    };
    
    function noPaidLeave(app) {
      var regHoursArry = [[8,8,8,8,8]];
      gsParams.resource.data = [];
      gsParams.resource.data.push(
        {
          "majorDimension": "COLUMNS",
          "range": "D16:D20",
          "values": regHoursArry,
        }
      );
    
      app.ask("Thank you.  I will log that you worked 8 hours everyday."
      + " You should be getting a confirmation email "
      + "within the next few minutes.  Have a great weekend!");
      var scopeGS = ['https://www.googleapis.com/auth/spreadsheets'];
      authorizeGS(scopeGS, updateTimesheet);
    };
    
    function paidLeaveOrOvertime(app) {
      console.log("here on paidLeaveOrOvertime")
      var daysArry = app.getArgument('nonRegDays');
      var hoursArry = app.getArgument('nonRegHours');
      var absenceArry = app.getArgument('types');
      var row = '';
      var arrNum = null;
      var col = '';
      var cellLeave = [];
      var textArry = [];
      var regHoursArry = [[8,8,8,8,8]];
      var day_tot = null;
      if (daysArry.length != hoursArry.length || daysArry.length != absenceArry.length || hoursArry.length != absenceArry.length) {
        app.ask("Your input was missing some requirements please list out the days in the format of 'day','hour','type'")
      }
      else {
        console.log("What is these arrays?", daysArry, hoursArry, absenceArry);
        for(var i = 0; i <= daysArry.length - 1; i++) {
          var arrNum = null;
          switch (daysArry[i]){
              case "Monday" :
                  row ='16';
                  arrNum = 0;
                  break;
              case "Tuesday":
                  row ='17';
                  arrNum = 1;
                  break;
              case "Wednesday":
               row ='18';
                  arrNum = 2;
                  break;
              case "Thursday":
               row ='19';
                  arrNum = 3;
                  break;
              case "Friday":
                  row ='20';
                  arrNum = 4;
                  break;
              default:
                  row = '';
          };
    
          if (absenceArry[i] === 'vacation') {
              day_tot = 8 - parseInt(hoursArry[i]);
              col = 'F';
              cellLeave.push({"position":col + row, "hour": parseInt(hoursArry[i])});
          }
          else if (absenceArry[i] === 'sick leave') {
            sickNoteNeeded = true;
            day_tot = 8 - parseInt(hoursArry[i]);
            col = 'G';
            cellLeave.push({"position":col + row, "hour": parseInt(hoursArry[i])});
          }
          else if (absenceArry[i] === 'overtime') {
            day_tot = 8;
            col = 'E';
            cellLeave.push({"position":col + row, "hour": parseInt(hoursArry[i])});
          }
          regHoursArry[0][arrNum] = day_tot;
        };
    
        gsParams.resource.data = [];
    
        for(let y of cellLeave) {
          gsParams.resource.data.push(
            {
              "majorDimension": "COLUMNS",
              "range": y.position,
              "values": [[y.hour]]
            }
          )
        };
    
        gsParams.resource.data.push(
          {
            "majorDimension": "COLUMNS",
            "range": "D16:D20",
            "values": regHoursArry,
          }
        );
    
        for(var x=0; x<= daysArry.length-1; x++) {
          textArry.push(daysArry[x] + " which had " + hoursArry[x] + " hours as " + absenceArry[x]);
        };
        text = textArry.join(" and ");

        if(sickNoteNeeded === true) {
          console.log("he was sick and needs a note!");
          getSickNote(app);
          if(gotIt === true) {
            app.ask("I see you have submitted a medical excuse. I will log that you worked 8 hours everyday except for " + text + "."
            + " You should be getting a confirmation email "
            + "within the next few minutes.  Have a great weekend!");
            var scopeGS = ['https://www.googleapis.com/auth/spreadsheets'];
            authorizeGS(scopeGS, updateTimesheet);
          }
          else if(gotIt == false) {
            app.ask("Records indicate you have missed over 5 sick days."
            + "Please take a picture of your medical excuse and text to 808-427-1650."
            + "Once I receive and verify the excuse, I will submit your hours.");
          }
        }
        else {
        app.ask("Thank you.  I will log that you worked 8 hours everyday except for " + text + "."
        + " You should be getting a confirmation email "
        + "within the next few minutes.  Have a great weekend!");
        var scopeGS = ['https://www.googleapis.com/auth/spreadsheets'];
        authorizeGS(scopeGS, updateTimesheet);
        }
      }
    };
    
    function paidLeave(app) {
      sickNoteNeeded = false;
      gotIt = false;
      var daysArry = app.getArgument('days');
      var hoursArry = app.getArgument('number');
      var absenceArry = app.getArgument('absence');
      if (daysArry.length != hoursArry.length || daysArry.length != absenceArry.length || hoursArry.length != absenceArry.length) {
        app.ask("Your input was missing some requirements please list out the days in the format of 'day','hour','type'")
      }
      else {
        var row = '';
        var arrNum = null;
        var col = '';
        var cellLeave = [];
        var textArry = [];
        var regHoursArry = [[8,8,8,8,8]];
        for(var i = 0; i <= daysArry.length - 1; i++) {
          var arrNum = null;
          var day_tot = 8 - parseInt(hoursArry[i]);
          switch (daysArry[i]){
              case "Monday" :
                  row ='16';
                  arrNum = 0;
                  break;
              case "Tuesday":
                  row ='17';
                  arrNum = 1;
                  break;
              case "Wednesday":
               row ='18';
                  arrNum = 2;
                  break;
              case "Thursday":
               row ='19';
                  arrNum = 3;
                  break;
              case "Friday":
                  row ='20';
                  arrNum = 4;
                  break;
              default:
                  row = '';
          };
    
          if (absenceArry[i] === 'vacation') {
              col = 'F';
              cellLeave.push({"position":col + row, "hour": parseInt(hoursArry[i])});
          }
          else if (absenceArry[i] === 'sick leave') {
            console.log("He said he is sick!!")
            sickNoteNeeded = true;
            col = 'G';
            cellLeave.push({"position":col + row, "hour": parseInt(hoursArry[i])});
          };
          regHoursArry[0][arrNum] = day_tot;
        };
    
        gsParams.resource.data = [];
    
        for(let y of cellLeave) {
          gsParams.resource.data.push(
            {
              "majorDimension": "COLUMNS",
              "range": y.position,
              "values": [[y.hour]]
            }
          )
        };
    
        gsParams.resource.data.push(
          {
            "majorDimension": "COLUMNS",
            "range": "D16:D20",
            "values": regHoursArry,
          }
        );
    
        for(var x=0; x<= daysArry.length-1; x++) {
          textArry.push(daysArry[x] + " which had " + hoursArry[x] + " hours as " + absenceArry[x]);
        };
        text = textArry.join(" and ");
    
        if(sickNoteNeeded === true) {
          console.log("I realize he was sick and needs a note!");
          getSickNote(app);
          if(gotIt === true) {
            app.ask("I see you have submitted a medical excuse. I will log that you worked 8 hours everyday except for " + text + "."
            + " You should be getting a confirmation email "
            + "within the next few minutes.  Have a great weekend!");
            var scopeGS = ['https://www.googleapis.com/auth/spreadsheets'];
            authorizeGS(scopeGS, updateTimesheet);
          }
          else if(gotIt == false) {
            app.ask("Records indicate you have missed over 5 sick days."
          + "Please take a picture of your medical excuse and text to 808-427-1650."
          + "Once I receive and verify the excuse, I will submit your hours.");
          }
        }
        else {
          app.ask("Thank you.  I will log that you worked 8 hours everyday except for " + text + "."
          + " You should be getting a confirmation email "
          + "within the next few minutes.  Have a great weekend!");
          var scopeGS = ['https://www.googleapis.com/auth/spreadsheets'];
          authorizeGS(scopeGS, updateTimesheet);
        }
      }
    }
    
    function getSickNote(app) {
      if(sickNoteFound === true) {
        gotIt = true;
        return gotIt;
      }
      else if(sickNoteFound === false) {
        gotIt = false;
        return gotIt;
      }
    
    };
    
    const actionMap = new Map();
    actionMap.set('sendPassphrase', sendPassphrase);
    actionMap.set('reset', reset);
    actionMap.set('input.welcome', welcome);
    actionMap.set('quotes', quote);
    actionMap.set('input.unknown', searchRR);
    actionMap.set('general.projectImpact', generalProjectImpact);
    actionMap.set('contacts', contacts);
    actionMap.set('announcements', announcements);
    actionMap.set('calendar', calendar);
    actionMap.set('general-help', help);
    actionMap.set('exemptGetHours', getHours);
    actionMap.set('getHours', getHours);
    actionMap.set('paidLeave', paidLeave);
    actionMap.set('noPaidLeave', noPaidLeave);
    actionMap.set('microsoftBot', microsoftBot);
    actionMap.set('paidLeaveOrOvertime', paidLeaveOrOvertime);
    actionMap.set('sendTicket', sendTicket);
    actionMap.set('identity', identity);
    actionMap.set('sound', sound);
    app.handleRequest(actionMap);    
  }
};

  