import { Injectable } from '@angular/core';
import { Actions, Effect, ofType } from '@ngrx/effects';
import { of } from 'rxjs';
import { map, mergeMap, catchError } from 'rxjs/operators';
import * as customerOrdersActions from './customer-orders-actions';
import { CustomersService, CustomerOrdersService } from 'src/app/gateway-api/services';
import { Customer, CustomerOrdersModel } from 'src/app/gateway-api/models';

@Injectable()
export class CustomerOrdersEffects {
    constructor(
        private actions$: Actions,
        private customersService: CustomersService,
        private customerOrdersService: CustomerOrdersService
    ) {
        this.customersService.rootUrl = "http://localhost:5000";
        this.customerOrdersService.rootUrl = "http://localhost:5000";
    }

    @Effect()
    getCustomers = this.actions$.pipe(
        ofType(customerOrdersActions.Actions.GetCustomers),
        mergeMap(() =>
            this.customersService.GetCustomers()
                .pipe(
                    map((response: Customer[]) => {
                        return new customerOrdersActions.GetCustomersSuccess(response)
                    }),
                    catchError((err) => {
                        return of(new customerOrdersActions.GetCustomersFailure());
                    })
                )));

    getOrders = this.actions$.pipe(
        ofType(customerOrdersActions.Actions.GetCustomerOrders),
        mergeMap((action: customerOrdersActions.GetCustomerOrders) =>
            this.customerOrdersService.GetCustomerOrders({ customerId: action.payload })
                .pipe(
                    map((response: CustomerOrdersModel) => {
                        return new customerOrdersActions.GetCustomerOrdersSuccess(response)
                    }),
                    catchError((err) => {
                        return of(new customerOrdersActions.GetCustomerOrdersFailure());
                    })
                )));
}
