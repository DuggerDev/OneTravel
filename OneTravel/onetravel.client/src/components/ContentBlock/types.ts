export interface ContentBlockProps {
  icon: string;
  title: string;
  content: string;
  section?: {
    title: string;
    content: string;
    icon: string;
  }[];
  button?: (
    | {
        title: string;
        color?: undefined;
        // onClick: Function;
      }
    | {
        title: string;
        color: string;
        // onClick: Function;
      }
  )[];
  id: string;
  direction: "left" | "right";
}
