import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProductDetailComponent } from './Components/product/product-detail/product-detail.component';
import { ProductComponent } from './Components/product/product.component';
import { RegisterComponent } from './Components/register/register.component';
import { LoginComponent } from './login/login.component';
import { AuthguardGuard } from './Services/Auth/authguard.guard';


const routes: Routes = [
  {
    path: ' ', redirectTo: 'login'
  }, {
    path: 'login', component: LoginComponent
  },
  {
    path: 'register', component: RegisterComponent
  },

  {
    path: 'product',
    component: ProductComponent,
    pathMatch: 'full',
    canActivate:[AuthguardGuard]
  },
  {
    path: 'product/:id',
    component: ProductDetailComponent,
    pathMatch: 'full',
    canActivate:[AuthguardGuard]
  },
  {
    path: 'create-product',
    component: ProductDetailComponent,
    pathMatch: 'full',
    canActivate:[AuthguardGuard]
  },];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
