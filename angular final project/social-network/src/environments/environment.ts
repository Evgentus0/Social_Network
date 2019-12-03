// This file can be replaced during build by using the `fileReplacements` array.
// `ng build --prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.
// var prefix:string="https://socialnetworkpl.azurewebsites.net";
var prefix:string="https://localhost:44348/";

export const environment = {
  production: false,
  currentUserUrl: prefix+"api/users",
  searchUser: prefix+"api/users/search",
  loginUser:prefix+"/token",
  publications:prefix+"api/publications",
  messageHeader:prefix+"api/messageheaders",
  message:prefix+"api/message",  
  additional:prefix+"api/additional",
};

/*
 * For easier debugging in development mode, you can import the following file
 * to ignore zone related error stack frames such as `zone.run`, `zoneDelegate.invokeTask`.
 *
 * This import should be commented out in production mode because it will have a negative impact
 * on performance if an error is thrown.
 */
// import 'zone.js/dist/zone-error';  // Included with Angular CLI.
