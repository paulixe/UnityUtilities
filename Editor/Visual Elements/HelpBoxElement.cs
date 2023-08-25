using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace EditorUtilities
{
    /// <summary>
    /// Good source for creating new custom visual elements <see href="https://forum.unity.com/threads/ui-builder-and-custom-elements.785129/"/>
    /// </summary>
    public class HelpBoxElement : VisualElement
    {

        private const float margin = 2;
        private const float padding = 1;
        private Image imageElement;
        private Label labelElement;

        private string helpText = "Hello World!";
        public string HelpText
        {
            get => helpText;
            set
            {
                helpText = value;
                labelElement.text = helpText;
            }
        }

        private MessageType messageType;
        public MessageType MessageType
        {
            get => messageType;
            set
            {
                string iconName = value switch
                {
                    MessageType.Warning => "console.warnicon",
                    MessageType.Info => "console.infoicon",
                    MessageType.Error => "console.erroricon",
                    _ => "console.infoicon"
                };
                imageElement.image = EditorGUIUtility.FindTexture(iconName);
                messageType = value;
            }
        }
        public new class UxmlFactory : UxmlFactory<HelpBoxElement, UxmlTraits> { }

        public new class UxmlTraits : VisualElement.UxmlTraits
        {
            private UxmlEnumAttributeDescription<MessageType> m_Message_Type = new UxmlEnumAttributeDescription<MessageType> { name = "message-type", defaultValue = MessageType.Info };
            private UxmlStringAttributeDescription m_Text = new UxmlStringAttributeDescription { name = "help-Text", defaultValue = "default" };

            public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
            {
                get { yield break; }
            }

            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);
                var ate = ve as HelpBoxElement;

                ate.MessageType = m_Message_Type.GetValueFromBag(bag, cc);
                ate.HelpText = m_Text.GetValueFromBag(bag, cc);
            }
        }


        public HelpBoxElement() : this(MessageType.Info) { }
        public HelpBoxElement(MessageType messageType)
        {
            style.flexDirection = FlexDirection.Row;
            style.alignItems = Align.Center;
            style.marginBottom = margin;
            style.marginRight = margin;
            style.marginLeft = margin;
            style.marginTop = margin;
            style.paddingTop = padding;
            style.paddingBottom = padding;
            style.paddingRight = padding;
            style.paddingLeft = padding;

            AddToClassList("unity-box");

            labelElement = new Label(HelpText);
            imageElement = new Image() { scaleMode = ScaleMode.ScaleToFit };
            Add(imageElement);
            Add(labelElement);

            MessageType = messageType;
        }
    }
}
