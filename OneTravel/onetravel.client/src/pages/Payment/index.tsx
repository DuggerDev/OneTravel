import { lazy } from "react";
import PaymentContent from "./PaymentContent.json";

const Contact = lazy(() => import("../../components/ContactForm"));
const Container = lazy(() => import("../../common/Container"));


const PaymentPage = () => {
  return (
    <Container>
      <Contact
        title={PaymentContent.title}
        content={PaymentContent.text}
        id="contact"
      />
    </Container>
  );
};

export default PaymentPage;
