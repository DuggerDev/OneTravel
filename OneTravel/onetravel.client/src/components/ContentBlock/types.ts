import { TFunction } from "react-i18next";
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
  t: TFunction;
  id: string;
  direction: "left" | "right";
}
