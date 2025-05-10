import { lazy } from "react";

import NoPageContent from "./NoPageContent.json";


const Container = lazy(() => import("../../common/Container"));
const ContentBlock = lazy(() => import("../../components/ContentBlock"));

const NoPage = () => {
  return (
    <Container>
      <ContentBlock
        direction="left"
        title={NoPageContent.title}
        content={NoPageContent.text}
        icon="product-launch.svg"
        id="product"
      />
    </Container>
  );
};

export default NoPage;
