using Grado_Cerrado.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Grado_Cerrado.Domain.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ai_evaluation> ai_evaluations { get; set; }

    public virtual DbSet<ai_generation> ai_generations { get; set; }

    public virtual DbSet<answer_medium> answer_media { get; set; }

    public virtual DbSet<attempt> attempts { get; set; }

    public virtual DbSet<legal_doc> legal_docs { get; set; }

    public virtual DbSet<question> questions { get; set; }

    public virtual DbSet<question_model_answer> question_model_answers { get; set; }

    public virtual DbSet<question_option> question_options { get; set; }

    public virtual DbSet<question_source> question_sources { get; set; }

    public virtual DbSet<question_tts_cache> question_tts_caches { get; set; }

    public virtual DbSet<quiz_session> quiz_sessions { get; set; }

    public virtual DbSet<subject> subjects { get; set; }

    public virtual DbSet<topic> topics { get; set; }

    public virtual DbSet<user> users { get; set; }

    public virtual DbSet<user_badge> user_badges { get; set; }

    public virtual DbSet<user_level> user_levels { get; set; }

    public virtual DbSet<user_setting> user_settings { get; set; }

    public virtual DbSet<user_topic_stat> user_topic_stats { get; set; }

    public virtual DbSet<vector_chunks_ref> vector_chunks_refs { get; set; }

    public virtual DbSet<vw_session_accuracy> vw_session_accuracies { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=pg-gradocerrado.postgres.database.azure.com;Database=gradocerrado;Username=adminuser;Password=Derecho2024.;Port=5432;Trust Server Certificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ai_evaluation>(entity =>
        {
            entity.HasKey(e => e.id).HasName("ai_evaluations_pkey");

            entity.HasIndex(e => e.attempt_id, "idx_ai_eval_attempt");

            entity.Property(e => e.id).HasDefaultValueSql("uuid_via_md5()");
            entity.Property(e => e.created_at).HasDefaultValueSql("now()");
            entity.Property(e => e.raw).HasColumnType("jsonb");

            entity.HasOne(d => d.attempt).WithMany(p => p.ai_evaluations)
                .HasForeignKey(d => d.attempt_id)
                .HasConstraintName("ai_evaluations_attempt_id_fkey");
        });

        modelBuilder.Entity<ai_generation>(entity =>
        {
            entity.HasKey(e => e.id).HasName("ai_generations_pkey");

            entity.Property(e => e.id).HasDefaultValueSql("uuid_via_md5()");
            entity.Property(e => e.created_at).HasDefaultValueSql("now()");
            entity.Property(e => e.raw_response).HasColumnType("jsonb");

            entity.HasOne(d => d.question).WithMany(p => p.ai_generations)
                .HasForeignKey(d => d.question_id)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("ai_generations_question_id_fkey");
        });

        modelBuilder.Entity<answer_medium>(entity =>
        {
            entity.HasKey(e => e.id).HasName("answer_media_pkey");

            entity.HasIndex(e => e.attempt_id, "idx_answer_media_attempt");

            entity.Property(e => e.id).HasDefaultValueSql("uuid_via_md5()");
            entity.Property(e => e.created_at).HasDefaultValueSql("now()");

            entity.HasOne(d => d.attempt).WithMany(p => p.answer_media)
                .HasForeignKey(d => d.attempt_id)
                .HasConstraintName("answer_media_attempt_id_fkey");
        });

        modelBuilder.Entity<attempt>(entity =>
        {
            entity.HasKey(e => e.id).HasName("attempts_pkey");

            entity.HasIndex(e => e.created_at, "idx_attempts_created");

            entity.HasIndex(e => e.question_id, "idx_attempts_question");

            entity.HasIndex(e => e.quiz_session_id, "idx_attempts_session");

            entity.Property(e => e.id).HasDefaultValueSql("uuid_via_md5()");
            entity.Property(e => e.created_at).HasDefaultValueSql("now()");
            entity.Property(e => e.respuesta_opciones).HasColumnType("jsonb");
            entity.Property(e => e.source).HasDefaultValueSql("'text'::text");

            entity.HasOne(d => d.question).WithMany(p => p.attempts)
                .HasForeignKey(d => d.question_id)
                .HasConstraintName("attempts_question_id_fkey");

            entity.HasOne(d => d.quiz_session).WithMany(p => p.attempts)
                .HasForeignKey(d => d.quiz_session_id)
                .HasConstraintName("attempts_quiz_session_id_fkey");
        });

        modelBuilder.Entity<legal_doc>(entity =>
        {
            entity.HasKey(e => e.id).HasName("legal_docs_pkey");

            entity.Property(e => e.id).HasDefaultValueSql("uuid_via_md5()");
            entity.Property(e => e.created_at).HasDefaultValueSql("now()");
        });

        modelBuilder.Entity<question>(entity =>
        {
            entity.HasKey(e => e.id).HasName("questions_pkey");

            entity.HasIndex(e => new { e.topic_id, e.dificultad, e.tipo }, "idx_questions_topic").HasFilter("is_active");

            entity.Property(e => e.id).HasDefaultValueSql("uuid_via_md5()");
            entity.Property(e => e.created_at).HasDefaultValueSql("now()");
            entity.Property(e => e.dificultad).HasDefaultValue((short)3);
            entity.Property(e => e.is_active).HasDefaultValue(true);

            entity.HasOne(d => d.topic).WithMany(p => p.questions)
                .HasForeignKey(d => d.topic_id)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("questions_topic_id_fkey");
        });

        modelBuilder.Entity<question_model_answer>(entity =>
        {
            entity.HasKey(e => e.question_id).HasName("question_model_answer_pkey");

            entity.ToTable("question_model_answer");

            entity.Property(e => e.question_id).ValueGeneratedNever();
            entity.Property(e => e.refs)
                .HasDefaultValueSql("'{}'::jsonb")
                .HasColumnType("jsonb");

            entity.HasOne(d => d.question).WithOne(p => p.question_model_answer)
                .HasForeignKey<question_model_answer>(d => d.question_id)
                .HasConstraintName("question_model_answer_question_id_fkey");
        });

        modelBuilder.Entity<question_option>(entity =>
        {
            entity.HasKey(e => e.id).HasName("question_options_pkey");

            entity.HasIndex(e => e.question_id, "idx_qopts_question");

            entity.Property(e => e.id).HasDefaultValueSql("uuid_via_md5()");
            entity.Property(e => e.is_correct).HasDefaultValue(false);
            entity.Property(e => e.opt_key).HasMaxLength(1);

            entity.HasOne(d => d.question).WithMany(p => p.question_options)
                .HasForeignKey(d => d.question_id)
                .HasConstraintName("question_options_question_id_fkey");
        });

        modelBuilder.Entity<question_source>(entity =>
        {
            entity.HasKey(e => new { e.question_id, e.collection, e.point_id }).HasName("question_sources_pkey");

            entity.Property(e => e.payload).HasColumnType("jsonb");

            entity.HasOne(d => d.question).WithMany(p => p.question_sources)
                .HasForeignKey(d => d.question_id)
                .HasConstraintName("question_sources_question_id_fkey");
        });

        modelBuilder.Entity<question_tts_cache>(entity =>
        {
            entity.HasKey(e => e.id).HasName("question_tts_cache_pkey");

            entity.ToTable("question_tts_cache");

            entity.HasIndex(e => e.question_id, "idx_tts_cache_q");

            entity.HasIndex(e => new { e.question_id, e.voice, e.rate, e.text_hash }, "question_tts_cache_question_id_voice_rate_text_hash_key").IsUnique();

            entity.Property(e => e.id).HasDefaultValueSql("uuid_via_md5()");
            entity.Property(e => e.created_at).HasDefaultValueSql("now()");
            entity.Property(e => e.rate).HasDefaultValueSql("1.0");

            entity.HasOne(d => d.question).WithMany(p => p.question_tts_caches)
                .HasForeignKey(d => d.question_id)
                .HasConstraintName("question_tts_cache_question_id_fkey");
        });

        modelBuilder.Entity<quiz_session>(entity =>
        {
            entity.HasKey(e => e.id).HasName("quiz_sessions_pkey");

            entity.Property(e => e.id).HasDefaultValueSql("uuid_via_md5()");
            entity.Property(e => e.started_at).HasDefaultValueSql("now()");

            entity.HasOne(d => d.user).WithMany(p => p.quiz_sessions)
                .HasForeignKey(d => d.user_id)
                .HasConstraintName("quiz_sessions_user_id_fkey");
        });

        modelBuilder.Entity<subject>(entity =>
        {
            entity.HasKey(e => e.id).HasName("subjects_pkey");

            entity.Property(e => e.id).HasDefaultValueSql("uuid_via_md5()");
        });

        modelBuilder.Entity<topic>(entity =>
        {
            entity.HasKey(e => e.id).HasName("topics_pkey");

            entity.Property(e => e.id).HasDefaultValueSql("uuid_via_md5()");

            entity.HasOne(d => d.parent_topic).WithMany(p => p.Inverseparent_topic)
                .HasForeignKey(d => d.parent_topic_id)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("topics_parent_topic_id_fkey");

            entity.HasOne(d => d.subject).WithMany(p => p.topics)
                .HasForeignKey(d => d.subject_id)
                .HasConstraintName("topics_subject_id_fkey");
        });

        modelBuilder.Entity<user>(entity =>
        {
            entity.HasKey(e => e.id).HasName("users_pkey");

            entity.HasIndex(e => e.email, "users_email_key").IsUnique();

            entity.Property(e => e.id).HasDefaultValueSql("uuid_via_md5()");
            entity.Property(e => e.created_at).HasDefaultValueSql("now()");
        });

        modelBuilder.Entity<user_badge>(entity =>
        {
            entity.HasKey(e => e.id).HasName("user_badges_pkey");

            entity.Property(e => e.id).HasDefaultValueSql("uuid_via_md5()");
            entity.Property(e => e.granted_at).HasDefaultValueSql("now()");

            entity.HasOne(d => d.user).WithMany(p => p.user_badges)
                .HasForeignKey(d => d.user_id)
                .HasConstraintName("user_badges_user_id_fkey");
        });

        modelBuilder.Entity<user_level>(entity =>
        {
            entity.HasKey(e => e.user_id).HasName("user_levels_pkey");

            entity.Property(e => e.user_id).ValueGeneratedNever();
            entity.Property(e => e.level).HasDefaultValue(1);
            entity.Property(e => e.xp).HasDefaultValue(0);

            entity.HasOne(d => d.user).WithOne(p => p.user_level)
                .HasForeignKey<user_level>(d => d.user_id)
                .HasConstraintName("user_levels_user_id_fkey");
        });

        modelBuilder.Entity<user_setting>(entity =>
        {
            entity.HasKey(e => e.user_id).HasName("user_settings_pkey");

            entity.Property(e => e.user_id).ValueGeneratedNever();
            entity.Property(e => e.accesibilidad)
                .HasDefaultValueSql("'{}'::jsonb")
                .HasColumnType("jsonb");
            entity.Property(e => e.idioma).HasDefaultValueSql("'es-CL'::text");
            entity.Property(e => e.tts_rate).HasDefaultValueSql("1.0");
            entity.Property(e => e.tts_voice).HasDefaultValueSql("'es-CL-JavieraNeural'::text");
            entity.Property(e => e.tz).HasDefaultValueSql("'America/Santiago'::text");
            entity.Property(e => e.updated_at).HasDefaultValueSql("now()");

            entity.HasOne(d => d.user).WithOne(p => p.user_setting)
                .HasForeignKey<user_setting>(d => d.user_id)
                .HasConstraintName("user_settings_user_id_fkey");
        });

        modelBuilder.Entity<user_topic_stat>(entity =>
        {
            entity.HasKey(e => new { e.user_id, e.topic_id }).HasName("user_topic_stats_pkey");

            entity.HasIndex(e => new { e.user_id, e.topic_id }, "idx_user_topic_stats");

            entity.Property(e => e.aciertos).HasDefaultValue(0);
            entity.Property(e => e.last_update).HasDefaultValueSql("now()");
            entity.Property(e => e.preguntas).HasDefaultValue(0);

            entity.HasOne(d => d.topic).WithMany(p => p.user_topic_stats)
                .HasForeignKey(d => d.topic_id)
                .HasConstraintName("user_topic_stats_topic_id_fkey");

            entity.HasOne(d => d.user).WithMany(p => p.user_topic_stats)
                .HasForeignKey(d => d.user_id)
                .HasConstraintName("user_topic_stats_user_id_fkey");
        });

        modelBuilder.Entity<vector_chunks_ref>(entity =>
        {
            entity.HasKey(e => e.id).HasName("vector_chunks_ref_pkey");

            entity.ToTable("vector_chunks_ref");

            entity.HasIndex(e => new { e.doc_id, e.chunk_idx }, "idx_vector_chunks_doc");

            entity.HasIndex(e => e.topic_id, "idx_vector_chunks_topic");

            entity.HasIndex(e => new { e.collection, e.point_id }, "vector_chunks_ref_collection_point_id_key").IsUnique();

            entity.Property(e => e.id).HasDefaultValueSql("uuid_via_md5()");
            entity.Property(e => e.created_at).HasDefaultValueSql("now()");

            entity.HasOne(d => d.doc).WithMany(p => p.vector_chunks_refs)
                .HasForeignKey(d => d.doc_id)
                .HasConstraintName("vector_chunks_ref_doc_id_fkey");

            entity.HasOne(d => d.subject).WithMany(p => p.vector_chunks_refs)
                .HasForeignKey(d => d.subject_id)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("vector_chunks_ref_subject_id_fkey");

            entity.HasOne(d => d.topic).WithMany(p => p.vector_chunks_refs)
                .HasForeignKey(d => d.topic_id)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("vector_chunks_ref_topic_id_fkey");
        });

        modelBuilder.Entity<vw_session_accuracy>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_session_accuracy");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
