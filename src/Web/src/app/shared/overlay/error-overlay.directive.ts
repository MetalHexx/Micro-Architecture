import { Directive, ElementRef, OnInit, Input } from '@angular/core';
import { OverlayRef } from '@angular/cdk/overlay';
import { Observable } from 'rxjs';
import { ComponentPortal } from '@angular/cdk/portal';
import { DynamicOverlay } from './dynamic-overlay';
import { OverlayType } from './models/overlay-type';
import { OverlayContainerComponent } from './overlay-container/overlay-container.component';

@Directive({
  selector: '[errorOverlay]'
})
export class ErrorOverlayDirective implements OnInit {
  @Input('errorOverlay') toggler$: Observable<boolean>;
  @Input('errorMessage') message: string;
  @Input('errorType') type: OverlayType;

  private overlayRef: OverlayRef;

  constructor(
    private host: ElementRef,
    private dynamicOverlay: DynamicOverlay
  ) {}

  ngOnInit() {
    this.overlayRef = this.dynamicOverlay.createWithDefaultConfig(
      this.host.nativeElement
    );

    this.toggler$.subscribe(show => {
        if (show) {
            const portal = new ComponentPortal(OverlayContainerComponent);
            let ref = this.overlayRef.attach(portal);
            ref.instance.message = this.message;
            ref.instance.type = this.type;
        } 
        else {
            this.overlayRef.detach();  
        }
    }); 
  }
}
