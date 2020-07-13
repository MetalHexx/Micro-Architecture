import { Directive, ElementRef, OnInit, Input } from '@angular/core';
import { OverlayRef } from '@angular/cdk/overlay';
import { Observable, merge, empty } from 'rxjs';
import { ComponentPortal } from '@angular/cdk/portal';
import { DynamicOverlay } from './dynamic-overlay';
import { filter, delay, switchMap } from 'rxjs/operators';
import { OverlayType } from './models/overlay-type';
import { OverlayContainerComponent } from './overlay-container/overlay-container.component';

@Directive({
  selector: '[loadingOverlay]'
})
export class LoadingOverlayDirective implements OnInit {
    @Input('loadingOverlay') toggler$: Observable<boolean>;
    @Input('loadingMessage') message: string;
    @Input('loadingType') type: OverlayType;

  private overlayRef: OverlayRef;

  constructor(
    private host: ElementRef,
    private dynamicOverlay: DynamicOverlay
  ) {}

  ngOnInit() {
    this.overlayRef = this.dynamicOverlay.createWithDefaultConfig(
      this.host.nativeElement
    );

    this.renderLoadingObservable();
  }

  renderLoadingObservable(){
    const enableToggle$ = this.toggler$.pipe(
        filter(toggle => toggle === true));    

    const disableToggle$ = this.toggler$.pipe(
        filter(toggle => toggle === false));

    const delayEnableToggle$ = this.toggler$.pipe(
        delay(500),
        switchMap(toggle => toggle === true ? enableToggle$ : empty())        
    );

    const switchStream$ = merge(
        delayEnableToggle$,
        disableToggle$);         

    switchStream$.subscribe(show => {
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
