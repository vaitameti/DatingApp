import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanDeactivate, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { MemberEditComponent } from '../members/member-edit/member-edit.component';

@Injectable({
  providedIn: 'root'
})
export class PreventUnsavedChangesGuard implements CanDeactivate<unknown> {
  canDeactivate(
    component: MemberEditComponent):  boolean {
      if(component.editForm.dirty){
        return confirm('Are you sure you want to continue? Any unsaved changes will be lost!');//this will give yes or no option
      }
    return true;// if yes they  will not stay in the form and changes will be unsaved
  }
  
}
