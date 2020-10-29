using System;
using UnityEngine;
using UnityEngine.UI;

namespace Common {
    public class FlexibleGridLayout : LayoutGroup {
        public int rows;
        public int columns;
        public Vector2 cellSize;
        public Vector2 spacing;

        public enum FitType {
            Uniform,
            Rows,
            Columns
        }

        public FitType fitType;

        public override void SetLayoutHorizontal() {
            base.CalculateLayoutInputHorizontal();
            
            ApplyFitType();
            
            var rect = rectTransform.rect;
            var parentWidth = rect.width;
            var parentHeight = rect.height;

            var spacingOffsetX = spacing.x / columns * (columns - 1);
            var spacingOffsetY = spacing.y / rows * 2;

            var paddingOffsetX = padding.left / (float) columns - padding.right / (float) columns;
            var paddingOffsetY = padding.top / (float) rows - padding.bottom / (float) rows;

            var cellWidth = parentWidth / columns - spacingOffsetX - paddingOffsetX;
            var cellHeight = parentHeight / rows - spacingOffsetY - paddingOffsetY;

            cellSize.x = cellWidth;
            cellSize.y = cellHeight;

            for (var i = 0; i < rectChildren.Count; i++) {
                var rowCount = i / columns;
                var columnCount = i % columns;

                var item = rectChildren[i];

                var spacingX = spacing.x * columnCount;
                var spacingY = spacing.y * columnCount;

                var xPos = cellSize.x * columnCount + spacingX + padding.left;
                var yPos = cellSize.y * rowCount + spacingY + padding.top;

                SetChildAlongAxis(item, 0, xPos, cellSize.x);
                SetChildAlongAxis(item, 1, yPos, cellSize.y);
            }
        }

        private void ApplyFitType() {
            if (fitType == FitType.Rows) {
                rows = Mathf.CeilToInt(transform.childCount / (float) columns);
            }else if (fitType == FitType.Columns) {
                columns = Mathf.CeilToInt(transform.childCount / (float) rows);
            }else if (fitType == FitType.Uniform) {
                var sqrt = Mathf.Sqrt(transform.childCount);
                rows = Mathf.CeilToInt(sqrt);
                columns = Mathf.CeilToInt(sqrt);
            }
        }

        public override void SetLayoutVertical() { }

        public override void CalculateLayoutInputVertical() { }
    }
}