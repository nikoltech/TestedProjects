import { NgModule } from "@angular/core";
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from "@angular/common/http";
import { Routes, RouterModule } from "@angular/router";

import { AppComponent } from './app.component';
import { ProductListComponent } from './product/product-list.component';
import { ProductFormComponent } from './product/product-form.component';
import { ProductCreateComponent } from './product/product-create.component';
import { ProductEditComponent } from './product/product-edit.component';
import { NotFoundComponent } from './not-found.component';

import { DataService } from './data.service';

// определение маршрутов
const appRoutes: Routes = [
    { path: '', component: ProductListComponent },
    { path: 'create', component: ProductCreateComponent },
    { path: 'edit/:id', component: ProductEditComponent },
    { path: '**', component: NotFoundComponent },
];

@NgModule({
    imports: [BrowserModule, FormsModule, HttpClientModule, RouterModule.forRoot(appRoutes)],
    declarations: [AppComponent, ProductListComponent, ProductCreateComponent, ProductEditComponent,
        ProductFormComponent, NotFoundComponent],
    providers: [DataService], // регистрация сервисов
    bootstrap: [AppComponent]
})
export class AppModule { }