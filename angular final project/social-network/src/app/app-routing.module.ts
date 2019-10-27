import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ProfileComponent } from './profile/profile.component';
import { SignInComponent } from './sign-in/sign-in.component';
import { RegisterComponent } from './register/register.component';
import { StartPageComponent } from './start-page/start-page.component';
import { MessengerComponent } from './messenger/messenger.component';
import { CorrespondenceComponent } from './correspondence/correspondence.component';
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


const routes: Routes = [
  {path:"", pathMatch:"full", redirectTo:"start-page"},
  {path:"profile", component: ProfileComponent},
  {path:"sign-in", component:SignInComponent},
  {path:"register", component:RegisterComponent},
  {path:"start-page", component:StartPageComponent},
  {path:"sign-in/register", redirectTo:"register"},
  {path:"start-page/sign-in", redirectTo:"sign-in"},
  {path:"start-page/register", redirectTo:"register"},
  {path:"messanger", component:MessengerComponent},
  {path:"correspondence/:id", component:CorrespondenceComponent},
  {path:"followers/:id", component:FollowersComponent},
  {path:"following/:id", component:FollowingComponent},
  {path:"add-publication", component:AddPublicationComponent},
  {path:"publication-edit/:id", component:PublicationEditComponent},
  {path:"user-profile/:id", component:UserProfileComponent},
  {path:"main-publications", component:MainPublicationComponent},
  {path:"add-messanger", component:AddMessangerComponent},
  {path:"all-users-delete", component:AllUsersDeleteComponent},
  {path:"error", component:ErrorViewComponent},
  {path:"add-following", component:AddFollowingComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
