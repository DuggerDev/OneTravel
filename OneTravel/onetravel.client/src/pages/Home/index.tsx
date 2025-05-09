import { lazy } from "react";
import HomePageContent from "./HomePageContent.json";

const Container = lazy(() => import("../../common/Container"));
const ContentBlock = lazy(() => import("../../components/ContentBlock"));

const Home = () => {
  return (
    <Container>
      <ContentBlock
        direction="right"
        title={HomePageContent.title}
        content={HomePageContent.text}
        button={HomePageContent.button}
        icon="developer.svg"
        id="intro"
      />
    </Container>
  );
};

export default Home;
