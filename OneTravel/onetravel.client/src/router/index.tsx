import { Suspense } from "react";
import {Routes, Route } from "react-router-dom";
import Header from "../components/Header";
import { Styles } from "../styles/styles";
import Home from "../pages/Home";
import TravelSearch from "../pages/TravelSearch";
import PaymentPage from "../pages/Payment";
import UnderDevelopment from "../pages/UnderDevelopment";


const Router = () => {
  return (
      <Suspense fallback={<div>Loading...</div>}>
        <Styles />
        <Header />
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/search" element={<TravelSearch />} />
          <Route path="/payment" element={<PaymentPage />} />

          {/* Other */}
          <Route path="*" element={<UnderDevelopment />} />

        </Routes>
      </Suspense>
  );
};

export default Router;
