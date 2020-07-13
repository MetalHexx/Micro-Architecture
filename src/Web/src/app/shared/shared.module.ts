import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MaterialModule } from '../material/material.module';
import { ErrorOverlayDirective } from './overlay/error-overlay.directive';
import { LoadingOverlayDirective } from './overlay/loading-overlay.directive';
import { OverlayContainerComponent } from './overlay/overlay-container/overlay-container.component';



@NgModule({
  exports:[ErrorOverlayDirective, LoadingOverlayDirective, OverlayContainerComponent],
  declarations: [ErrorOverlayDirective, LoadingOverlayDirective, OverlayContainerComponent],
  imports: [
    CommonModule,
    MaterialModule
  ],
  entryComponents: [OverlayContainerComponent]
})
export class SharedModule { } 
