﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using NarouViewer.API;

namespace NarouViewer
{
    public class NarouSearchView : Panel
    {
        private NarouAPI.GetParameter _model;
        public NarouAPI.GetParameter model
        {
            set
            {
                _model = value;
                ChangeModel();

            }
            get
            {
                return _model;
            }
        }

        private NovelDataListView _listModel;
        public NovelDataListView listModel
        {
            set
            {
                _listModel = value;
            }
            get
            {
                return _listModel;
            }
        }

        private SearchLabel searchLabel;
        private SearchTextBox searchTextBox;
        private ChoiceSearchWordButton choiceSearchWordButton;
        private ExclusionLabel exclusionLabel;
        private ExclusionTextBox exclusionTextBox;
        private ChoiceExclusionWordButton choiceExclusionWordButton;
        private SearchOptionLabel searchOptionLabel;
        private ChoiceGenreButton choiceGenreButton;
        private ChoiceDetailOption choiceDetailOption;
        private SearchButton searchButton;

        public NarouSearchView(NarouAPI.GetParameter model)
        {
            this.Location = new Point(3, 3);
            this.Name = "searchPanel";

            this.Controls.Add(this.searchLabel = new SearchLabel());
            this.Controls.Add(this.searchTextBox = new SearchTextBox());
            this.Controls.Add(this.choiceSearchWordButton = new ChoiceSearchWordButton());
            this.Controls.Add(this.exclusionLabel = new ExclusionLabel());
            this.Controls.Add(this.exclusionTextBox = new ExclusionTextBox());
            this.Controls.Add(this.choiceExclusionWordButton = new ChoiceExclusionWordButton());
            this.Controls.Add(this.searchOptionLabel = new SearchOptionLabel());
            this.Controls.Add(this.choiceGenreButton = new ChoiceGenreButton());
            this.Controls.Add(this.choiceDetailOption = new ChoiceDetailOption());
            this.Controls.Add(this.searchButton = new SearchButton());
            this.Controls.Add(this.listModel = new NovelDataListView(new List<NarouAPI.NovelData>()));
            this.Size = new Size(706, 185 + listModel.Size.Height);

            this.searchButton.Click += new EventHandler((object sender, EventArgs e) => Search());

            this.model = model;
            this.Search();
        }

        private void ChangeModel()
        {
            if (this.InvokeRequired)
            {
                this.Invoke((Action)ChangeModel);
                return;
            }

            if (model == null) return;

            model.word = searchTextBox.Text;
            model.notWord = exclusionTextBox.Text;
        }
        public void Search()
        {
            if (listModel == null) return;
            this.ChangeModel();

            Task.Run(async () =>
            {
                this.listModel.model = await NarouAPI.Get(model);

                Invoke((Action)(() =>
                {
                    this.Size = new Size(706, 185 + listModel.Size.Height);
                }));
            });
        }

        private class SearchLabel : Label
        {
            public SearchLabel()
            {
                this.Font = new Font("MS UI Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
                this.Location = new Point(3, 19);
                this.Name = "searchLabel";
                this.Size = new Size(50, 25);
                this.Text = "検索";
                this.TextAlign = ContentAlignment.MiddleCenter;

            }
        }
        private class SearchTextBox : TextBox
        {
            public SearchTextBox()
            {
                this.Cursor = Cursors.IBeam;
                this.Font = new Font("ＭＳ Ｐゴシック", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
                this.Location = new Point(56, 19);
                this.Name = "searchTextBox";
                this.Size = new Size(514, 25);

            }
        }
        private class ChoiceSearchWordButton : Button
        {
            public ChoiceSearchWordButton()
            {
                this.Location = new Point(573, 19);
                this.Name = "ChoiceSearchWordButton";
                this.Size = new Size(122, 25);
                this.Text = "+ 検索ワードを選択";
                this.UseVisualStyleBackColor = true;
            }
        }
        private class ExclusionLabel : Label
        {
            public ExclusionLabel()
            {
                this.Font = new Font("MS UI Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
                this.Location = new Point(3, 56);
                this.Name = "exclusionLabel";
                this.Size = new Size(50, 25);
                this.Text = "除外";
                this.TextAlign = ContentAlignment.MiddleCenter;
            }
        }
        private class ExclusionTextBox : TextBox
        {
            public ExclusionTextBox()
            {
                this.Cursor = Cursors.IBeam;
                this.Font = new Font("ＭＳ Ｐゴシック", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
                this.Location = new Point(56, 56);
                this.Name = "ExclusionTextBox";
                this.Size = new Size(514, 25);
            }
        }
        private class ChoiceExclusionWordButton : Button
        {
            public ChoiceExclusionWordButton()
            {
                this.Location = new Point(573, 56);
                this.Name = "ChoiceExclusionWordButton";
                this.Size = new Size(122, 25);
                this.Text = "+ 除外ワードを選択";
                this.UseVisualStyleBackColor = true;
            }
        }
        private class SearchOptionLabel : Label
        {
            public SearchOptionLabel()
            {
                this.Font = new Font("MS UI Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
                this.Location = new Point(311, 89);
                this.Name = "searchOptionLabel";
                this.Size = new Size(117, 23);
                this.Text = "検索条件設定 ：";
                this.TextAlign = ContentAlignment.MiddleCenter;
            }
        }
        private class ChoiceGenreButton : Button
        {
            public ChoiceGenreButton()
            {
                this.Font = new Font("ＭＳ Ｐゴシック", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
                this.Location = new Point(431, 87);
                this.Name = "choiceGenreButton";
                this.Size = new Size(130, 26);
                this.Text = "+ ジャンル選択";
                this.UseVisualStyleBackColor = true;
            }
        }
        private class ChoiceDetailOption : Button
        {
            public ChoiceDetailOption()
            {
                this.Font = new Font("ＭＳ Ｐゴシック", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
                this.Location = new Point(567, 87);
                this.Name = "ChoiceDetailOption";
                this.Size = new Size(130, 26);
                this.Text = "+ 詳細条件設定";
                this.UseVisualStyleBackColor = true;
            }
        }
        private class SearchButton : Button
        {
            public SearchButton()
            {
                this.Font = new Font("ＭＳ Ｐゴシック", 12F, FontStyle.Bold, GraphicsUnit.Point, 128);
                this.Location = new Point(100, 122);
                this.Name = "searchButton";
                this.Size = new Size(500, 33);
                this.Text = "検索";
                this.UseVisualStyleBackColor = true;
            }
        }
    }
}
