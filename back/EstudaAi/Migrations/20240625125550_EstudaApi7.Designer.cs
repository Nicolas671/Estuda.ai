﻿// <auto-generated />
using System;
using EstudaAi.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace senaiApiTwo7.Migrations
{
    [DbContext(typeof(GestaoDBContext))]
    [Migration("20240625125550_EstudaApi7")]
    partial class EstudaApi7
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("EstudaAi.Entities.Correcao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("DificuldadeQuestao")
                        .HasColumnType("longtext");

                    b.Property<string>("RespostaQuestao")
                        .HasColumnType("longtext");

                    b.Property<int?>("questaoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("questaoId");

                    b.ToTable("Correcaos");
                });

            modelBuilder.Entity("EstudaAi.Entities.Estudante", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Data_de_Nasc")
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("Hora_de_Login")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Nome")
                        .HasColumnType("longtext");

                    b.Property<string>("Senha")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Estudantes");
                });

            modelBuilder.Entity("EstudaAi.Entities.Pontuacao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Acertos")
                        .HasColumnType("longtext");

                    b.Property<int?>("resultFinalId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("resultFinalId");

                    b.ToTable("Pontuacaos");
                });

            modelBuilder.Entity("EstudaAi.Entities.Questao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Disciplina")
                        .HasColumnType("longtext");

                    b.Property<string>("Pergunta")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Questaos");
                });

            modelBuilder.Entity("EstudaAi.Entities.Registro", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("estudanteId")
                        .HasColumnType("int");

                    b.Property<int?>("pontuacaoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("estudanteId");

                    b.HasIndex("pontuacaoId");

                    b.ToTable("Registros");
                });

            modelBuilder.Entity("EstudaAi.Entities.Resposta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("RespostaUser")
                        .HasColumnType("longtext");

                    b.Property<int?>("estudanteId")
                        .HasColumnType("int");

                    b.Property<int?>("questaoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("estudanteId");

                    b.HasIndex("questaoId");

                    b.ToTable("Respostas");
                });

            modelBuilder.Entity("EstudaAi.Entities.ResultFinal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("correcaoId")
                        .HasColumnType("int");

                    b.Property<int?>("respostaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("correcaoId");

                    b.HasIndex("respostaId");

                    b.ToTable("ResultFinals");
                });

            modelBuilder.Entity("EstudaAi.Entities.Correcao", b =>
                {
                    b.HasOne("EstudaAi.Entities.Questao", "questao")
                        .WithMany()
                        .HasForeignKey("questaoId");

                    b.Navigation("questao");
                });

            modelBuilder.Entity("EstudaAi.Entities.Pontuacao", b =>
                {
                    b.HasOne("EstudaAi.Entities.ResultFinal", "resultFinal")
                        .WithMany()
                        .HasForeignKey("resultFinalId");

                    b.Navigation("resultFinal");
                });

            modelBuilder.Entity("EstudaAi.Entities.Registro", b =>
                {
                    b.HasOne("EstudaAi.Entities.Estudante", "estudante")
                        .WithMany()
                        .HasForeignKey("estudanteId");

                    b.HasOne("EstudaAi.Entities.Pontuacao", "pontuacao")
                        .WithMany()
                        .HasForeignKey("pontuacaoId");

                    b.Navigation("estudante");

                    b.Navigation("pontuacao");
                });

            modelBuilder.Entity("EstudaAi.Entities.Resposta", b =>
                {
                    b.HasOne("EstudaAi.Entities.Estudante", "estudante")
                        .WithMany()
                        .HasForeignKey("estudanteId");

                    b.HasOne("EstudaAi.Entities.Questao", "questao")
                        .WithMany()
                        .HasForeignKey("questaoId");

                    b.Navigation("estudante");

                    b.Navigation("questao");
                });

            modelBuilder.Entity("EstudaAi.Entities.ResultFinal", b =>
                {
                    b.HasOne("EstudaAi.Entities.Correcao", "correcao")
                        .WithMany()
                        .HasForeignKey("correcaoId");

                    b.HasOne("EstudaAi.Entities.Resposta", "resposta")
                        .WithMany()
                        .HasForeignKey("respostaId");

                    b.Navigation("correcao");

                    b.Navigation("resposta");
                });
#pragma warning restore 612, 618
        }
    }
}
