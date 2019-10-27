import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {FormsModule} from "@angular/forms";

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { UserService } from './shared/services/user.service';
import { AuthService } from './shared/services/auth.service';
import { PublicationService } from './shared/services/publication.service';
import { MessageHeaderService } from './shared/services/message-header.service';
import { MessageService } from './shared/services/message.service';
import { AdditionalService } from './shared/services/additional.service';
import { TokenInterceptor } from './shared/interseptor/token.interseptor';
import { CorrespondenceComponent } from './correspondence/correspondence.component';
import { MessengerComponent } from './messenger/messenger.component';
import { ProfileComponent } from './profile/profile.component';
import { PublicationComponent } from './publication/publication.component';
import { RegisterComponent } from './register/register.component';
import { SignInComponent } from './sign-in/sign-in.component';
import { StartPageComponent } from './start-page/start-page.component';
import { FollowersComponent } from './followers/followers.component';
import { FollowingComponent } from './following/following.component';
import { AddPublicationComponent } from './add-publication/add-publication.component';
import { PublicationEditComponent } from './publication-edit/publication-edit.component';
import { UserProfileComponent } from './user-profile/user-profile.component';
import { MainPublicationComponent } from './main-publication/main-publication.component';
import { AddMessangerComponent } from './add-messanger/add-messanger.component';
import { AllUsersDeleteComponent } from './all-users-delete/all-users-delete.component';
import { ErrorViewComponent } from './error-view/error-view.component';
import { AddFollowingComponent } from './add-following/add-following.component';
import { ModalDialogComponent } from './modal-dialog/modal-dialog.component';

@NgModule({
  declarations: [
    AppComponent,
    CorrespondenceComponent,
    MessengerComponent,
    ProfileComponent,
    PublicationComponent,
    RegisterComponent,
    SignInComponent,
    StartPageComponent,
    FollowersComponent,
    FollowingComponent,
    AddPublicationComponent,
    PublicationEditComponent,
    UserProfileComponent,
    MainPublicationComponent,
    AddMessangerComponent,
    AllUsersDeleteComponent,
    ErrorViewComponent,
    AddFollowingComponent,
    ModalDialogComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
  ],
  providers: [
    UserService,
    AuthService,
    PublicationService,
    MessageHeaderService,
    MessageService,
    AdditionalService,
    {
      provide:HTTP_INTERCEPTORS,
      useClass:TokenInterceptor,
      multi:true,
    }

  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
