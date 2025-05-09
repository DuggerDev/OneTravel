import { lazy } from "react";

import UnderDevelopmentContent from "./UnderDevelopmentContent.json";


const Container = lazy(() => import("../../common/Container"));
const ContentBlock = lazy(() => import("../../components/ContentBlock"));

const UnderDevelopment = () => {
  return (
    <Container>
      <ContentBlock
        direction="left"
        title={UnderDevelopmentContent.title}
        content={UnderDevelopmentContent.text}
        icon="waving.svg"
        id="product"
      />
    </Container>
  );
};

export default UnderDevelopment;
